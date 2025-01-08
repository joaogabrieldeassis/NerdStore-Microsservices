using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NerdStoreEnterprise.Cart.Api.Data;
using NerdStoreEnterprise.Cart.Api.Models;
using NerdStoreEnterprise.Services.Controllers;
using NerdStoreEnterprise.Services.Users;

namespace NerdStoreEnterprise.Cart.Api.Controllers;


[Route("[controller]")]
[Authorize]
public class CartController : MainController
{
    private readonly IAspNetUser _user;
    private readonly CartContext _context;

    public CartController(IAspNetUser user, CartContext context)
    {
        _user = user;
        _context = context;
    }

    [HttpGet("cart")]
    public async Task<ClientCart> GetCart()
    {
        return await GetCartClient() ?? new ClientCart();
    }

    [HttpPost("cart")]
    public async Task<IActionResult> AddCartItem(CartItem item)
    {
        var cart = await GetCartClient();

        if (cart == null)
            HandleNewCart(item);
        else
            HandleExistingCart(cart, item);

        if (!OperationIsValid()) return CustomResponse();

        await PersistData();
        return CustomResponse();
    }

    [HttpPut("cart/{productId}")]
    public async Task<IActionResult> UpdateCartItem(Guid productId, CartItem item)
    {
        var cart = await GetCartClient();
        var cartItem = await GetValidatedCartItem(productId, cart, item);
        if (cartItem == null) return CustomResponse();

        cart.UpdateUnits(cartItem, item.Quantity);

        ValidateCart(cart);
        if (!OperationIsValid()) return CustomResponse();

        _context.CartItems.Update(cartItem);
        _context.ClientCart.Update(cart);

        await PersistData();
        return CustomResponse();
    }

    [HttpDelete("cart/{productId}")]
    public async Task<IActionResult> RemoveCartItem(Guid productId)
    {
        var cart = await GetCartClient();

        var cartItem = await GetValidatedCartItem(productId, cart);
        if (cartItem == null) return CustomResponse();

        ValidateCart(cart);
        if (!OperationIsValid()) return CustomResponse();

        cart.RemoveItem(cartItem);

        _context.CartItems.Remove(cartItem);
        _context.ClientCart.Update(cart);

        await PersistData();
        return CustomResponse();
    }

    private async Task<ClientCart> GetCartClient()
    {
        return await _context.ClientCart
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.ClientId == _user.GetUserId());
    }
    private void HandleNewCart(CartItem item)
    {
        var cart = new ClientCart(_user.GetUserId());
        cart.AddItem(item);

        ValidateCart(cart);
        _context.ClientCart.Add(cart);
    }
    private void HandleExistingCart(ClientCart cart, CartItem item)
    {
        var existingProductItem = cart.ExistingCartItem(item);

        cart.AddItem(item);
        ValidateCart(cart);

        if (existingProductItem)
        {
            _context.CartItems.Update(cart.GetByProductId(item.ProductId));
        }
        else
        {
            _context.CartItems.Add(item);
        }

        _context.ClientCart.Update(cart);
    }

    private async Task<CartItem> GetValidatedCartItem(Guid productId, ClientCart cart, CartItem item = null)
    {
        if (item != null && productId != item.ProductId)
        {
            AddProcessingError("The item does not match the specified one");
            return null;
        }

        if (cart == null)
        {
            AddProcessingError("Cart not found");
            return null;
        }

        var cartItem = await _context.CartItems.FirstOrDefaultAsync(i => i.CartId == cart.Id && i.ProductId == productId);

        if (cartItem == null || !cart.ExistingCartItem(cartItem))
        {
            AddProcessingError("The item is not in the cart");
            return null;
        }

        return cartItem;
    }

    private async Task PersistData()
    {
        var result = await _context.SaveChangesAsync();
        if (result <= 0) AddProcessingError("Unable to persist data to the database");
    }

    private bool ValidateCart(ClientCart cart)
    {
        if (cart.IsValid()) return true;

        cart.ValidationResult.Errors.ToList().ForEach(e => AddProcessingError(e.ErrorMessage));
        return false;
    }

}