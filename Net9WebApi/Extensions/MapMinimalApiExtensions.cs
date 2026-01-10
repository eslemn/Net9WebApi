using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Net9WebApi.DTOs;
using Net9WebApi.Services.Interfaces;
using Net9WebApi.Wrappers;

namespace Net9WebApi.Extensions
{
    public static class MapMinimalApiExtensions
    {
        public static void MapMinimalEndpoints(this WebApplication app)
        {
            // Category Endpoints
            var categories = app.MapGroup("/minimal/categories").WithTags("Minimal Categories");

            categories.MapGet("/", async (ICategoryService service) =>
            {
                var data = await service.GetAllAsync();
                return Results.Ok(ApiResponse<List<CategoryDto>>.SuccessResponse(data));
            })
            .Produces<ApiResponse<List<CategoryDto>>>(StatusCodes.Status200OK);

            categories.MapGet("/{id}", async (int id, ICategoryService service) =>
            {
                var data = await service.GetByIdAsync(id);
                if (data == null)
                    return Results.NotFound(ApiResponse<CategoryDto>.FailResponse("Category not found"));
                return Results.Ok(ApiResponse<CategoryDto>.SuccessResponse(data));
            })
            .Produces<ApiResponse<CategoryDto>>(StatusCodes.Status200OK)
            .Produces<ApiResponse<CategoryDto>>(StatusCodes.Status404NotFound);

            categories.MapPost("/", async ([FromBody] CreateCategoryDto dto, ICategoryService service) =>
            {
                var data = await service.CreateAsync(dto);
                return Results.Created($"/minimal/categories/{data.Id}", ApiResponse<CategoryDto>.SuccessResponse(data, "Category created successfully"));
            })
            .RequireAuthorization(new AuthorizeAttribute { Roles = "Admin" })
            .Produces<ApiResponse<CategoryDto>>(StatusCodes.Status201Created);

            categories.MapPut("/{id}", async (int id, [FromBody] UpdateCategoryDto dto, ICategoryService service) =>
            {
                var success = await service.UpdateAsync(id, dto);
                if (!success)
                    return Results.NotFound(ApiResponse<bool>.FailResponse("Category not found"));
                return Results.Ok(ApiResponse<bool>.SuccessResponse(true, "Category updated successfully"));
            })
            .RequireAuthorization(new AuthorizeAttribute { Roles = "Admin" })
            .Produces<ApiResponse<bool>>(StatusCodes.Status200OK)
            .Produces<ApiResponse<bool>>(StatusCodes.Status404NotFound);

            categories.MapDelete("/{id}", async (int id, ICategoryService service) =>
            {
                var success = await service.DeleteAsync(id);
                if (!success)
                    return Results.NotFound(ApiResponse<bool>.FailResponse("Category not found"));
                return Results.Ok(ApiResponse<bool>.SuccessResponse(true, "Category deleted successfully"));
            })
            .RequireAuthorization(new AuthorizeAttribute { Roles = "Admin" })
            .Produces<ApiResponse<bool>>(StatusCodes.Status200OK)
            .Produces<ApiResponse<bool>>(StatusCodes.Status404NotFound);


            // Product Endpoints
            var products = app.MapGroup("/minimal/products").WithTags("Minimal Products");

            products.MapGet("/", async (IProductService service) =>
            {
                var data = await service.GetAllAsync();
                return Results.Ok(ApiResponse<List<ProductDto>>.SuccessResponse(data));
            })
            .Produces<ApiResponse<List<ProductDto>>>(StatusCodes.Status200OK);

            products.MapGet("/{id}", async (int id, IProductService service) =>
            {
                var data = await service.GetByIdAsync(id);
                if (data == null)
                    return Results.NotFound(ApiResponse<ProductDto>.FailResponse("Product not found"));
                return Results.Ok(ApiResponse<ProductDto>.SuccessResponse(data));
            })
            .Produces<ApiResponse<ProductDto>>(StatusCodes.Status200OK)
            .Produces<ApiResponse<ProductDto>>(StatusCodes.Status404NotFound);

            products.MapPost("/", async ([FromBody] CreateProductDto dto, IProductService service) =>
            {
                var data = await service.CreateAsync(dto);
                return Results.Created($"/minimal/products/{data.Id}", ApiResponse<ProductDto>.SuccessResponse(data, "Product created successfully"));
            })
            .RequireAuthorization(new AuthorizeAttribute { Roles = "Admin" })
            .Produces<ApiResponse<ProductDto>>(StatusCodes.Status201Created);

            products.MapPut("/{id}", async (int id, [FromBody] UpdateProductDto dto, IProductService service) =>
            {
                var success = await service.UpdateAsync(id, dto);
                if (!success)
                    return Results.NotFound(ApiResponse<bool>.FailResponse("Product not found"));
                return Results.Ok(ApiResponse<bool>.SuccessResponse(true, "Product updated successfully"));
            })
            .RequireAuthorization(new AuthorizeAttribute { Roles = "Admin" })
            .Produces<ApiResponse<bool>>(StatusCodes.Status200OK)
            .Produces<ApiResponse<bool>>(StatusCodes.Status404NotFound);

            products.MapDelete("/{id}", async (int id, IProductService service) =>
            {
                var success = await service.DeleteAsync(id);
                if (!success)
                    return Results.NotFound(ApiResponse<bool>.FailResponse("Product not found"));
                return Results.Ok(ApiResponse<bool>.SuccessResponse(true, "Product deleted successfully"));
            })
            .RequireAuthorization(new AuthorizeAttribute { Roles = "Admin" })
            .Produces<ApiResponse<bool>>(StatusCodes.Status200OK)
            .Produces<ApiResponse<bool>>(StatusCodes.Status404NotFound);


            // User Endpoints
            var users = app.MapGroup("/minimal/users").WithTags("Minimal Users");

            users.MapGet("/", async (IUserService service) =>
            {
                var data = await service.GetAllAsync();
                return Results.Ok(ApiResponse<List<UserDto>>.SuccessResponse(data));
            })
            .Produces<ApiResponse<List<UserDto>>>(StatusCodes.Status200OK);

            users.MapGet("/{id}", async (int id, IUserService service) =>
            {
                var data = await service.GetByIdAsync(id);
                if (data == null)
                    return Results.NotFound(ApiResponse<UserDto>.FailResponse("User not found"));
                return Results.Ok(ApiResponse<UserDto>.SuccessResponse(data));
            })
            .Produces<ApiResponse<UserDto>>(StatusCodes.Status200OK)
            .Produces<ApiResponse<UserDto>>(StatusCodes.Status404NotFound);

            users.MapPost("/", async ([FromBody] CreateUserDto dto, IUserService service) =>
            {
                var data = await service.CreateAsync(dto);
                return Results.Created($"/minimal/users/{data.Id}", ApiResponse<UserDto>.SuccessResponse(data, "User created successfully"));
            })
            .RequireAuthorization(new AuthorizeAttribute { Roles = "Admin" })
            .Produces<ApiResponse<UserDto>>(StatusCodes.Status201Created);

            users.MapPut("/{id}", async (int id, [FromBody] UpdateUserDto dto, IUserService service) =>
            {
                var success = await service.UpdateAsync(id, dto);
                if (!success)
                    return Results.NotFound(ApiResponse<bool>.FailResponse("User not found"));
                return Results.Ok(ApiResponse<bool>.SuccessResponse(true, "User updated successfully"));
            })
            .RequireAuthorization(new AuthorizeAttribute { Roles = "Admin" })
            .Produces<ApiResponse<bool>>(StatusCodes.Status200OK)
            .Produces<ApiResponse<bool>>(StatusCodes.Status404NotFound);

            users.MapDelete("/{id}", async (int id, IUserService service) =>
            {
                var success = await service.DeleteAsync(id);
                if (!success)
                    return Results.NotFound(ApiResponse<bool>.FailResponse("User not found"));
                return Results.Ok(ApiResponse<bool>.SuccessResponse(true, "User deleted successfully"));
            })
            .RequireAuthorization(new AuthorizeAttribute { Roles = "Admin" })
            .Produces<ApiResponse<bool>>(StatusCodes.Status200OK)
            .Produces<ApiResponse<bool>>(StatusCodes.Status404NotFound);


            // Review Endpoints
            var reviews = app.MapGroup("/minimal/reviews").WithTags("Minimal Reviews");

            reviews.MapGet("/", async (IReviewService service) =>
            {
                var data = await service.GetAllAsync();
                return Results.Ok(ApiResponse<List<ReviewDto>>.SuccessResponse(data));
            })
            .Produces<ApiResponse<List<ReviewDto>>>(StatusCodes.Status200OK);

            reviews.MapGet("/{id}", async (int id, IReviewService service) =>
            {
                var data = await service.GetByIdAsync(id);
                if (data == null)
                    return Results.NotFound(ApiResponse<ReviewDto>.FailResponse("Review not found"));
                return Results.Ok(ApiResponse<ReviewDto>.SuccessResponse(data));
            })
            .Produces<ApiResponse<ReviewDto>>(StatusCodes.Status200OK)
            .Produces<ApiResponse<ReviewDto>>(StatusCodes.Status404NotFound);

            reviews.MapPost("/", async ([FromBody] CreateReviewDto dto, IReviewService service) =>
            {
                var data = await service.CreateAsync(dto);
                return Results.Created($"/minimal/reviews/{data.Id}", ApiResponse<ReviewDto>.SuccessResponse(data, "Review created successfully"));
            })
            .Produces<ApiResponse<ReviewDto>>(StatusCodes.Status201Created);

            reviews.MapPut("/{id}", async (int id, [FromBody] UpdateReviewDto dto, IReviewService service) =>
            {
                var success = await service.UpdateAsync(id, dto);
                if (!success)
                    return Results.NotFound(ApiResponse<bool>.FailResponse("Review not found"));
                return Results.Ok(ApiResponse<bool>.SuccessResponse(true, "Review updated successfully"));
            })
            .RequireAuthorization(new AuthorizeAttribute { Roles = "Admin" })
            .Produces<ApiResponse<bool>>(StatusCodes.Status200OK)
            .Produces<ApiResponse<bool>>(StatusCodes.Status404NotFound);

            reviews.MapDelete("/{id}", async (int id, IReviewService service) =>
            {
                var success = await service.DeleteAsync(id);
                if (!success)
                    return Results.NotFound(ApiResponse<bool>.FailResponse("Review not found"));
                return Results.Ok(ApiResponse<bool>.SuccessResponse(true, "Review deleted successfully"));
            })
            .RequireAuthorization(new AuthorizeAttribute { Roles = "Admin" })
            .Produces<ApiResponse<bool>>(StatusCodes.Status200OK)
            .Produces<ApiResponse<bool>>(StatusCodes.Status404NotFound);

            // Nested Endpoints (Products -> Reviews, Users -> Reviews)
            app.MapGet("/minimal/products/{id}/reviews", async (int id, IReviewService service) =>
            {
                var data = await service.GetByProductIdAsync(id);
                return Results.Ok(ApiResponse<List<ReviewDto>>.SuccessResponse(data));
            })
            .WithTags("Minimal Products")
            .Produces<ApiResponse<List<ReviewDto>>>(StatusCodes.Status200OK);

            app.MapGet("/minimal/users/{id}/reviews", async (int id, IReviewService service) =>
            {
                var data = await service.GetByUserIdAsync(id);
                return Results.Ok(ApiResponse<List<ReviewDto>>.SuccessResponse(data));
            })
            .WithTags("Minimal Users")
            .Produces<ApiResponse<List<ReviewDto>>>(StatusCodes.Status200OK);
        }
    }
}
