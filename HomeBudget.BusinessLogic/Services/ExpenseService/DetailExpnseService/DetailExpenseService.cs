using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeBudget.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace HomeBudget.BusinessLogic.Services.ExpenseService.DetailExpnseService
{
    public class DetailExpenseService
    {
        private readonly AppDbContext _context;
        detailResponseModel model; 

        public DetailExpenseService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<detailResponseModel> GetDetailExpenseAsync(int id)
        {
            var detailData = await _context.TblBudgets.FirstOrDefaultAsync(x => x.BudgetId == id);

            if(detailData is null)
            {
                model = new detailResponseModel
                {
                    IsSuccess = false,
                    Message = "No data Found"
                };
            }

            var expenseData = await _context.TblExpenses.Where(x => x.BudgetId == id).ToListAsync();

            if (expenseData is null)
            {
                model = new detailResponseModel
                {
                    IsSuccess = false,
                    Message = "No data Found"
                };
            }
            var expenseList = new List<getAllExpenseResponseModel>();

            foreach (var item in expenseData)
            {
                expenseList.Add(new getAllExpenseResponseModel
                {
                    Name = item.Name,
                    Amount = item.Amount,
                    CreatedDate = item.CreatedDate
                });
            }

           return model = new detailResponseModel
                            {
                                IsSuccess = true,
                                Message = "Success",
                                Data = detailData,
                                ExpenseList = expenseList
                            };

        }

        public class detailResponseModel() : BasedResponseModel
        {
            public TblBudget Data { get; set; }
            public List<getAllExpenseResponseModel> ExpenseList { get; set; }
        }

        public class getAllExpenseResponseModel()
        {
            public string Name { get; set; } = null!;

            public decimal Amount { get; set; }

            public DateTime CreatedDate { get; set; }
        }
    }
}
