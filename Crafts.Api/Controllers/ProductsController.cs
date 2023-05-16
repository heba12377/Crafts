﻿using Crafts.BL.Dtos.CouponDtos;
using Crafts.BL.Dtos.ProductDtos;
using Crafts.BL.Managers.ProductManager;
using Crafts.DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Crafts.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsManager _productsManager;
        

        public ProductsController(IProductsManager productManager)
        {
            _productsManager = productManager;
            
        }

        [HttpGet]
        public ActionResult<List<ProductReadDto>> GetAll()
        {
            return _productsManager.GetAll();
        }
  
        [HttpGet]
        [Route("{id:int}")]
        public ActionResult<ProductReadDto> GetById(int id) 
        {
            var product = _productsManager.GetById(id); 
            return product;
        }

        [HttpPost]
        public async Task<ActionResult> Add(ProductAddDto productDto) {
            await _productsManager.Add(productDto);
            return Ok(productDto);
        }

        [HttpPut]
        [Route("image/{id:int}")] //"api/categories/uploadimage" (Edit Image)
        public ActionResult AddImage([FromForm] ProductImgAddDto productImgAddDto, int id)
        {
            try
            {  
                _productsManager.AddImage(productImgAddDto, id);
                var msg = new GeneralResponse($"Image Updated Successfully with Id: {id}");
                var res = new { msg, id, productImgAddDto };
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
