using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using lotteryAPI.Models;
using System.Xml.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace lotteryAPI.Controllers
{
    [Route("api/[controller]")]
    public class LotteryDrawController : Controller
    {
        private static List<LotteryDraw> draws = new List<LotteryDraw>
        {
            //new LotteryDraw
            //{
            //    NrOne = 1,
            //    NrTwo = 2,
            //    NrThree = 3,
            //    NrFour = 4,
            //    NrFive = 5,
            //    NrSix = 6,
            //    DrawDate = DateTime.Now
            //}
        };

        [HttpGet]
        public async Task<ActionResult<LotteryDraw>> lotteryDraw()
        {
            //LotteryDraw draw = new LotteryDraw
            //{
            //    NrOne = getRandomNr(),
            //    NrTwo = getRandomNr(),
            //    NrThree = getRandomNr(),
            //    NrFour = getRandomNr(),
            //    NrFive = getRandomNr(),
            //    NrSix = getRandomNr(),
            //    DrawDate = DateTime.Now
            //};
            return Ok(draws);
        }

        //public int getRandomNr()
        //{
        //    Random random = new Random();
        //    int ranNum = random.Next(1, 49);
        //    return ranNum;
        //}

        [HttpGet("{date}")]
        public async Task<ActionResult<LotteryDraw>> GetLotteryDraw(DateTime date)
        {
            var draw = draws.Find(d => d.DrawDate == date);
            if (draw == null)
            {
                return BadRequest("No lottery draw for this date found!");
            }
            else
            {
                return Ok(draw);
            }
            
        }

        [HttpPost]
        public async Task<ActionResult<LotteryDraw>> AddLotteryDraw([FromBody]LotteryDraw draw)
        {
            draws.Add(draw);
            return Ok(draws);
        }

        [HttpDelete("{date}")]
        public async Task<ActionResult<LotteryDraw>> DeleteLotteryDraw(DateTime date)
        {
            var draw = draws.Find(d => d.DrawDate == date);
            if (draw == null)
            {
                return BadRequest("No lottery draw for this date found!");
            }
            else
            {
                draws.Remove(draw);
                return Ok(draws);
            }

        }

        //// GET: api/values
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/values/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}

