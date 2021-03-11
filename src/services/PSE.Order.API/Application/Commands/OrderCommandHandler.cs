using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;
using MediatR;
using PSE.Core.Messages;
using PSE.Order.API.Application.DTOs;
using PSE.Order.API.Application.Events;
using PSE.Order.Domain.Orders;
using PSE.Order.Domain.Vouchers;
using PSE.Order.Domain.Vouchers.Specs;

namespace PSE.Order.API.Application.Commands
{
    public class OrderCommandHandler : CommandHandler,
        IRequestHandler<ExecuteOrderCommand, ValidationResult>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IVoucherRepository _voucherRepository;

        public OrderCommandHandler(IOrderRepository orderRepository, IVoucherRepository voucherRepository)
        {
            _orderRepository = orderRepository;
            _voucherRepository = voucherRepository;
        }

        public async Task<ValidationResult> Handle(ExecuteOrderCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var order = MappingOrder(message);

            if (!await ApplyVoucher(message, order)) return ValidationResult;

            if (!ValidateOrder(order)) return ValidationResult;

            // Mockup
            if (!ProcessPayment(order)) return ValidationResult;

            order.AuthorizeOrder();

            order.AddEvent(new OrderExecutedEvent(order.Id, order.CustomerId));

            _orderRepository.Add(order);

            // persist data order and voucher
            return await PersistToBase(_orderRepository.UnitOfWork);
        }

        private OrderCustomer MappingOrder(ExecuteOrderCommand message)
        {
            var address = new Address
            {
                Street = message.Address.Street,
                Number = message.Address.Number,
                Complement = message.Address.Complement,
                Neighborhoodty = message.Address.Neighborhoodty,
                ZipCode = message.Address.ZipCode,
                City = message.Address.City,
                State = message.Address.State
            };

            var order = new OrderCustomer(message.CustomerId, message.TotalValue, message.OrderItems.Select(OrderItemDTO.ToOrderItem).ToList(),
                 message.VoucherUsage, message.Discount);

            order.AssociateAddress(address);
            return order;
        }

        private async Task<bool> ApplyVoucher(ExecuteOrderCommand message, OrderCustomer order)
        {
            if (!message.VoucherUsage) return true;

            var voucher = await _voucherRepository.GetVoucherByCode(message.VoucherCode);
            if (voucher == null)
            {
                AddErrors("Voucher not found!");
                return false;
            }

            var voucherValidation = new VoucherValidation().Validate(voucher);
            if (!voucherValidation.IsValid)
            {
                voucherValidation.Errors.ToList().ForEach(m => AddErrors(m.ErrorMessage));
                return false;
            }

            order.AssociateVoucher(voucher);
            voucher.DebitStock();

            _voucherRepository.Update(voucher);

            return true;
        }

        private bool ValidateOrder(OrderCustomer order)
        {
            var orderValueOriginal = order.TotalValue;
            var orderDiscount = order.Discount;

            order.CalculateTotalValueOrder();

            if (order.TotalValue != orderValueOriginal)
            {
                AddErrors("The total order amount does not match the order calculation");
                return false;
            }

            if (order.Discount != orderDiscount)
            {
                AddErrors("The total amount does not match the order calculation");
                return false;
            }

            return true;
        }

        public bool ProcessPayment(OrderCustomer order)
        {
            return true;
        }
    }
}
