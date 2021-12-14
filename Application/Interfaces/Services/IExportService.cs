using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IExportService
    {
        Task<string> ExportToExcelAsync<TData>(IEnumerable<TData> data
               , Dictionary<string, Func<TData, object>> mappers
               , string sheetName = "Sheet1");
    }
}
