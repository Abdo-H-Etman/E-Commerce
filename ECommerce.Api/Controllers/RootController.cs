// using System;
// using Asp.Versioning;
// using ECommerce.Domain.Entities.LinkModels;
// using Microsoft.AspNetCore.Mvc;

// namespace ECommerce.Api.Controllers;

// [Route("api")]
// [ApiController]
// public class RootController : ControllerBase
// {
//     private readonly LinkGenerator _linkGenerator;

//     public RootController(LinkGenerator linkGenerator) => _linkGenerator = linkGenerator;

//     [HttpGet(Name = "GetRoot")]
//     public IActionResult GetRoot([FromHeader(Name = "Accept")] string mediaType)
//     {
//         if(mediaType.Contains("application/vnd.codemaze.apiroot"))
//         {
//             var response = new List<Link>
//             {
//                 new Link(
//                     _linkGenerator.GetUriByName(HttpContext, "GetRoot", new { })!,
//                     "self",
//                     "GET"
//                 ),
//                 new Link(
//                     _linkGenerator.GetUriByName(HttpContext, "GetUsers", new { })!,
//                     "users",
//                     "GET"
//                 ),
//             };
//             return Ok(response);
//         }
//         return NoContent();
//     }
// }
