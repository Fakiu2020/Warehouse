//using System.Threading;
//using System.Threading.Tasks;
//using AutoMapper;
//using BaseProject.Application.Infrastructure.Request.Queries.GetById;
//using BaseProject.Domain;
//using BaseProject.Persistence;
//using MediatR;
//using Microsoft.EntityFrameworkCore;
//using Whoever.Common.Exceptions;
//using System.Linq;
//using BaseProject.Application.Managers;
//using System.Collections.Generic;
//using OfficeOpenXml;
//using OfficeOpenXml.Style;
//using System.Drawing;
//using Microsoft.AspNetCore.Http;
//using System;
//using AutoMapper.QueryableExtensions;

//namespace BaseProject.Application.Separation.Queries.ExcelExport
//{
//    public class ExcelExportHandler : IRequestHandler<ExcelExportQuery, byte[]>
//    {
//        private readonly BaseProjectDbContext _context;
//        private readonly IMapper _mapper;
//        private readonly Color colorFromHex;
//        private readonly UserManager _userMananger;
//        private readonly IHttpContextAccessor _httpContextAccesor;
//        private readonly UserManager _userManager;

//        public ExcelExportHandler(BaseProjectDbContext db, IMapper mapper, UserManager user, IHttpContextAccessor httpContextAccesor, UserManager userManager)
//        {
//            _context = db;
//            _mapper = mapper;
//            _userMananger = user;
//            _httpContextAccesor = httpContextAccesor;
//            _userManager = userManager;
//            colorFromHex = System.Drawing.ColorTranslator.FromHtml("#1F487C");
//        }

//        public async Task<byte[]> Handle(ExcelExportQuery request, CancellationToken cancellationToken)
//        {
//            try
//            {
//                return await ExportToExcel(request.Description, request.DateFrom, request.DateTo);

//            }
//            catch (Exception ex)
//            {
                 
//                throw ex;
//            }
//        }

//        //public async Task<byte[]> ExportToExcel(string description, string dateFrom, string dateTo)
//        //{

//        //    var userId = int.Parse(_httpContextAccesor.HttpContext.User.Claims.FirstOrDefault(u => u.Type == "id").Value);
//        //    var user = await _context.Users.Where(x => x.Id == userId).FirstAsync();
//        //    var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
//        //    var superAdmin = await _userManager.IsInRoleAsync(user, "Super Admin");
//        //    var dateFromParser = Convert.ToDateTime(dateFrom);
//        //    var dateToParser = Convert.ToDateTime(dateTo);
//        //    var result = await _context.Separations.Include(x => x.User.Plant).Include(x => x.Product)
//        //                              .OrderByDescending(x => x.CreationTime)
//        //                              .Where(x =>
//        //                                          ((superAdmin || isAdmin) || x.UserId == user.Id) &&
//        //                                          (string.IsNullOrEmpty(description) || x.Description.Contains(description)) &&
//        //                                          (string.IsNullOrEmpty(dateFrom) || x.SeparationDate.Date >= dateFromParser.Date) &&
//        //                                          (string.IsNullOrEmpty(dateTo) || x.SeparationDate.Date <= dateToParser.Date)).ToListAsync();

 

//        //    using (var package = new ExcelPackage())
//        //    {
//        //        ExcelWorksheet workSheet = package.Workbook.Worksheets.Add("Separaciones");

//        //        workSheet.TabColor = System.Drawing.Color.Black;
//        //        workSheet.DefaultRowHeight = 12;
//        //        workSheet.Row(1).Height = 20;
//        //        workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
//        //        workSheet.Row(1).Style.Font.Bold = true;
//        //        string[] header = new string[] { "Descripción", "Producto", "Unidad", "Cantidad", "Planta", "Usuario" };
//        //        BuildHeader(workSheet, header);
//        //        BuildBody(result, workSheet);
//        //        workSheet.Cells[workSheet.Dimension.End.Row + 2, 1].Value = "Fecha de Impresión";
//        //        workSheet.Cells[workSheet.Dimension.End.Row + 2, 1].AutoFitColumns();
//        //        workSheet.Cells[workSheet.Dimension.End.Row , 2].Value = DateTime.Now.ToString("dd/MM/yyyy");
//        //        workSheet.Cells[workSheet.Dimension.End.Row , 2].AutoFitColumns();
//        //        return package.GetAsByteArray();
//        //    }
//        //}

//        private void BuildHeader(ExcelWorksheet workSheet, string[] header)
//        {

//            for (int i = 1; i <= header.Length; i++)
//            {
//                workSheet.Cells[1, i].Style.Fill.PatternType = ExcelFillStyle.Solid;
//                workSheet.Cells[1, i].Style.Fill.BackgroundColor.SetColor(colorFromHex);
//                workSheet.Cells[1, i].Style.Font.Color.SetColor(Color.White);
//                workSheet.Cells[1, i].Value = header[i - 1].ToUpper();
//                workSheet.Cells[1, i].Style.Border.BorderAround(ExcelBorderStyle.Medium);
//                workSheet.Cells[1, i].AutoFitColumns();
//            }
//        }

//        //private void BuildBody(List<Domain.Separation> result, ExcelWorksheet workSheet)
//        //{
//        //    int recordIndex = 2;
//        //    foreach (var item in result.ToList())
//        //    {
//        //        workSheet.Cells[recordIndex, 1].Value = item.Description;
//        //        workSheet.Cells[recordIndex, 2].Value = item.Product.Description;
//        //        workSheet.Cells[recordIndex, 3].Value = item.Product.MeasuresUnit;
//        //        workSheet.Cells[recordIndex, 4].Value = item.Quantity;
//        //        workSheet.Cells[recordIndex, 5].Value = item.User.Plant.Name;
//        //        workSheet.Cells[recordIndex, 6].Value = item.User.UserName;
//        //        recordIndex++;
//        //    }

//        //    for (int i = 1; i < 10; i++)
//        //    {
//        //        workSheet.Column(i).AutoFit();
//        //    }
//        //}


//    }
//}
