using API.DTOs;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers;

public class ProductsController : BaseApiController
{
	IGenericRepository<ProductBrand> _productBandsRepo;
	IGenericRepository<Product> _productRepo;
	IGenericRepository<ProductType> _productType;
	public IMapper _mapper;

	public ProductsController(IGenericRepository<ProductBrand> productBandsRepo,
	IGenericRepository<Product> productRepo,
	IGenericRepository<ProductType> productType,
	IMapper mapper)
	{
		_productBandsRepo = productBandsRepo;
		_productRepo = productRepo;
		_productType = productType;
		_mapper = mapper;
	}

	[HttpGet]
	[Cached(600)]
	public async Task<ActionResult<Pagination<ProductToReturnDTO>>>
		GetProducts([FromQuery] ProductSpecPrams productParams)
	{
		var spec = new ProductsWithTypesAndBrandsSpecification(productParams);
		var countSpec = new ProductWithFiltersForCountSpecification(productParams);
		var totalItems = await _productRepo.CountAsync(countSpec);
		var products = await _productRepo.ListAsync(spec);
		var data = _mapper.Map<IReadOnlyList<ProductToReturnDTO>>(products).ToList();

		return Ok(new Pagination<ProductToReturnDTO>(
					productParams.PageIndex,
					productParams.PageSize,
					totalItems, data));
	}

	[HttpGet("{id}")]
	[Cached(600)]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
	public async Task<ActionResult<ProductToReturnDTO>> GetProduct(int id)
	{
		var spec = new ProductsWithTypesAndBrandsSpecification(id);
		var product = await _productRepo.GetEntityWithSpec(spec);

		if (product == null) return NotFound(new ApiResponse(404));
		return _mapper.Map<Product, ProductToReturnDTO>(product);
	}

	[HttpGet("brands")]
	[Cached(600)]
	public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
	{
		return Ok(await _productBandsRepo.ListAllAsync());
	}

	[HttpGet("types")]
    [Cached(600)]
	public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductTypes()
	{
		return Ok(await _productType.ListAllAsync());
	}
}
