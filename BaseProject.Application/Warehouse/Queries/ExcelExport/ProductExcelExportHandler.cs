using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BaseProject.Application.Infrastructure.Request.Queries.GetById;
using BaseProject.Domain;
using BaseProject.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Whoever.Common.Exceptions;
using System.Linq;
using BaseProject.Application.Managers;
using System.Collections.Generic;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using Microsoft.AspNetCore.Http;
using System;
using AutoMapper.QueryableExtensions;

namespace BaseProject.Application.Product.Queries.ExcelExport
{
    public class ProductExcelExportHandler : IRequestHandler<ProductExcelExportQuery, byte[]>
    {
        private readonly BaseProjectDbContext _context;
        private readonly IMapper _mapper;
        private readonly Color colorFromHex;
        private readonly UserManager _userMananger;
        private readonly IHttpContextAccessor _httpContextAccesor;
        private readonly UserManager _userManager;

        public ProductExcelExportHandler(BaseProjectDbContext db, IMapper mapper, UserManager user, IHttpContextAccessor httpContextAccesor, UserManager userManager)
        {
            _context = db;
            _mapper = mapper;
            _userMananger = user;
            _httpContextAccesor = httpContextAccesor;
            _userManager = userManager;
            colorFromHex = System.Drawing.ColorTranslator.FromHtml("#1F487C");
        }

        public async Task<byte[]> Handle(ProductExcelExportQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var wareHouse = await _context.Warehouse.FirstOrDefaultAsync(x => x.WarehouseId == request.WarehouseId);
                if(wareHouse==null)
                    throw new System.ComponentModel.DataAnnotations.ValidationException("Warehouse not found");

                var bytes = Convert.FromBase64String(wareHouse.FileData.Replace("data:" + wareHouse.MymeType + ";base64,", ""));

                return bytes;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



    }
}
