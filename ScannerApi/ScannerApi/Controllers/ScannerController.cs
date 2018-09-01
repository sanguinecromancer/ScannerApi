using Microsoft.AspNetCore.Mvc;
using Scanner.Core.Services;
using ScannerApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ScannerApi.Controllers
{
    [Route("api/[controller]")]
    public class ScannerController : Controller
    {

        private readonly URLContext _context;
        private readonly ScannerService _scannerService;
        private readonly VirusScanService _virusScanService;

        public ScannerController(URLContext context, ScannerService scannerService, VirusScanService virusScanService)
        {
            // Initializing an in-memory database for saving requests and responses for measuring purposes
            _context = context;
            _scannerService = scannerService;
            _virusScanService = virusScanService;

            if (_context.URLItems.Count() == 0)
            {
                _context.URLItems.Add(new URLItem { Url = "http://foobar.com" });
                _context.SaveChanges();
            }
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Scan(URLItem item)
        {
            // Get url from user
            string stringUrl = item.Url;
            bool isUri = Uri.IsWellFormedUriString(stringUrl, UriKind.RelativeOrAbsolute);

            if (!isUri)
                return BadRequest("URL is invalid");

            // Throw an error if there is no response for this URL
            if (!_scannerService.GetResponse(stringUrl))
            {
                throw new Exception("Something is wrong with file");
            }

            if (_scannerService.GetFileSize() > 209715200)
                return BadRequest("File size is too big, should be below 200 MB");

            // Write the response to the user
            item.Result = _virusScanService.GetVirusScanResult();
            item.Sha1 = _scannerService.GetCheckSum();

            _context.URLItems.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetURL", new { id = item.Id, result = item.Result, sha1 = item.Sha1 }, item);
        }


        [HttpGet]
        public List<URLItem> GetAll()
        {
            return _context.URLItems.ToList();
        }

        [HttpGet("{id}", Name = "GetURL")]
        public URLItem GetById(long id)
        {
            var item = _context.URLItems.Find(id);
            if (item == null)
            {
                return null;
            }
            return item;
        }

    }
}
