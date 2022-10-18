using System;
using Microsoft.EntityFrameworkCore;
using lotteryAPI.Models;


namespace lotteryAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        //public DbSet<LotteryDraw> Draws { get; set; }
    }
}

