using System;
namespace lotteryAPI.Models
{
    public class LotteryDraw
    {
        public int NrOne { get; set; }
        public int NrTwo { get; set; }
        public int NrThree { get; set; }
        public int NrFour { get; set; }
        public int NrFive { get; set; }
        public int NrSix { get; set; }
        public DateTime DrawDate { get; set; }
    }
}

