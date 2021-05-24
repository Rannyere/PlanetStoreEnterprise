using System;
using System.Threading.Tasks;
using PSE.Cart.API.Services.gRPC;
using PSE.Sales.BFF.DTOs;

namespace PSE.Sales.BFF.Services.gRPC
{
    public interface ICartGrpcService
    {
        Task<CartCustomerDTO> GetCart();
    }

    public class CartGrpcService : ICartGrpcService
    {
        private readonly CartGrpcIntegration.CartGrpcIntegrationClient _cartGrpcClient;

        public CartGrpcService(CartGrpcIntegration.CartGrpcIntegrationClient cartGrpcClient)
        {
            _cartGrpcClient = cartGrpcClient;
        }

        public async Task<CartCustomerDTO> GetCart()
        {
            var response = await _cartGrpcClient.GetCartAsync(new GetCartRequest { });

            return MapCartCustomerProtoResponseToDTO(response);
        }

        private static CartCustomerDTO MapCartCustomerProtoResponseToDTO(CartCustomerResponse cartResponse)
        {
            var cartCustomerDTO = new CartCustomerDTO
            {
                TotalValue = (decimal)cartResponse.Totalvalue,
                Discount = (decimal)cartResponse.Discount,
                VoucherUsage = cartResponse.Voucherusage
            };

            if (cartResponse.Voucher != null)
            {
                cartCustomerDTO.Voucher = new VoucherDTO
                {
                    Code = cartResponse.Voucher.Code,
                    DiscountPercentage = (decimal?)cartResponse.Voucher.Discountpercentage,
                    DiscountValue = (decimal?)cartResponse.Voucher.Discountvalue,
                    DiscountType = cartResponse.Voucher.Discounttype
                };
            }

            foreach (var item in cartResponse.Items)
            {
                cartCustomerDTO.Items.Add(new ItemCartDTO
                {
                    Name = item.Name,
                    Image = item.Image,
                    ProductId = Guid.Parse(item.Productid),
                    Quantity = item.Quantity,
                    Value = (decimal)item.Value
                });
            }

            return cartCustomerDTO;
        }
    }
}
