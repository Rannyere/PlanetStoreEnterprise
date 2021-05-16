using System;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PSE.Cart.API.Data;
using PSE.Cart.API.Models;
using PSE.WebAPI.Core.User;

namespace PSE.Cart.API.Services.gRPC
{
    public class CartGrpcService : CartGrpcIntegration.CartGrpcIntegrationBase
    {
        private readonly ILogger<CartGrpcService> _logger;

        private readonly IAspNetUser _user;
        private readonly CartDbContext _cartDbContext;

        public CartGrpcService(ILogger<CartGrpcService> logger,
                               IAspNetUser user,
                               CartDbContext cartDbContext)
        {
            _logger = logger;
            _user = user;
            _cartDbContext = cartDbContext;
        }

        public override async Task<CartCustomerResponse> GetCart(GetCartRequest request, ServerCallContext context)
        {
            _logger.LogInformation("gRPC GetCart");

            var cart = await GetCartCustomer() ?? new CartCustomer();

            return MapCarrinhoClienteToProtoResponse(cart);
        }

        private async Task<CartCustomer> GetCartCustomer()
        {
            return await _cartDbContext.CartCustomers
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.CustomerId == _user.GetUserId());
        }

        private static CartCustomerResponse MapCarrinhoClienteToProtoResponse(CartCustomer cart)
        {
            var cartProto = new CartCustomerResponse
            {
                Id = cart.Id.ToString(),
                Customerid = cart.CustomerId.ToString(),
                Totalvalue = (double)cart.TotalValue,
                Discount = (double)cart.Discount,
                Voucherusage = cart.VoucherUsage,
            };

            if (cart.Voucher != null)
            {
                cartProto.Voucher = new VoucherResponse
                {
                    Code = cart.Voucher.Code,
                    Discountpercentage = (double?)cart.Voucher.DiscountPercentage ?? 0,
                    Discountvalue = (double?)cart.Voucher.DiscountValue ?? 0,
                    Discounttype = (int)cart.Voucher.DiscountType
                };
            }

            foreach (var item in cart.Items)
            {
                cartProto.Items.Add(new CartitemsResponse
                {
                    Id = item.Id.ToString(),
                    Name = item.Name,
                    Image = item.Image,
                    Productid = item.ProductId.ToString(),
                    Quantity = item.Quantity,
                    Value = (double)item.Value
                });
            }

            return cartProto;
        }
    }
}