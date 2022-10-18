using System;
using System.IO;
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
        // respository for lottery draws
        private static List<LotteryDraw> draws = new List<LotteryDraw>{};

        [HttpGet]
        public async Task<ActionResult<LotteryDraw>> lotteryDraw()
        {
            List<int> randomList = getRandomList();
            LotteryDraw draw = new LotteryDraw
            {
                NrOne = randomList.ElementAt(0),
                NrTwo = randomList.ElementAt(1),
                NrThree = randomList.ElementAt(2),
                NrFour = randomList.ElementAt(3),
                NrFive = randomList.ElementAt(4),
                NrSix = randomList.ElementAt(5),
                DrawDate = DateTime.Now
            };
            await saveDraw(draw);
            return Ok(draw);
        }

        private static List<int> getRandomList()
        {
            List<int> randomList = new List<int>();
            while (randomList.Count < 6)
            {
                int randNr = getRandomNr();
                if (!randomList.Contains(randNr))
                {
                    randomList.Add(randNr);
                }

            }
            return randomList;
        }

        private static int getRandomNr()
        {
            Random random = new Random();
            int ranNum = random.Next(1, 49);
            return ranNum;
        }

        [HttpPost]
        public async Task<ActionResult<LotteryDraw>> saveDraw([FromBody] LotteryDraw draw)
        {
            draws.Add(draw);
            updateDrawTextFile();
            return Ok(draws);
        }

        private static void updateDrawTextFile()
        {

            System.IO.File.Delete("/Users/Jessica/Projects/lotteryAPI/lotteryDraws.txt");
            // save draws in textfile
            try
            {
                // pass the filepath and filename to the StreamWriter Constructor
                StreamWriter sw = new StreamWriter("/Users/Jessica/Projects/lotteryAPI/lotteryDraws.txt");
                foreach (LotteryDraw d in draws)
                {
                    sw.WriteLine(d.NrOne + "," + d.NrTwo + "," + d.NrThree + "," + d.NrFour + "," + d.NrFive + "," + d.NrSix + "," + d.DrawDate);
                }
                // close the file
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }

        [HttpGet("{date}")]
        public async Task<ActionResult<LotteryDraw>> historyDraw(String date)
        {
            getLotteryDrawFromTextfile();

            var draw = draws.Find(d => d.DrawDate.ToString("MM/dd/yyyy") == date);
            if (draw == null)
            {
                return BadRequest("No lottery draw for this date found!");
            }
            else
            {
                return Ok(draw);
            }
            
        }

        private static void getLotteryDrawFromTextfile()
        {
            // reset draw list - not ideal
            draws = new List<LotteryDraw>();
            // get lottery draws from textfile
            String drawInFile;
            try
            {
                StreamReader sr = new StreamReader("/Users/Jessica/Projects/lotteryAPI/lotteryDraws.txt");
                // read the first line of text
                drawInFile = sr.ReadLine();
                // continue to read until the end of file is reached
                while (drawInFile != null)
                {
                    string[] drawParts = drawInFile.Split(',');
                    var drawTemp = new LotteryDraw
                    {
                        NrOne = Int32.Parse(drawParts[0]),
                        NrTwo = Int32.Parse(drawParts[1]),
                        NrThree = Int32.Parse(drawParts[2]),
                        NrFour = Int32.Parse(drawParts[3]),
                        NrFive = Int32.Parse(drawParts[4]),
                        NrSix = Int32.Parse(drawParts[5]),
                        DrawDate = DateTime.Parse(drawParts[6])
                    };
                    draws.Add(drawTemp);
                    drawInFile = sr.ReadLine();
                }
                //close the file
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }

        [HttpDelete("{date}")]
        public async Task<ActionResult<LotteryDraw>> resetDraw(String date)
        {
            getLotteryDrawFromTextfile();
            
            DateTime dateInput = DateTime.ParseExact(date, "MM-dd-yyyy", null);
            int dateDiff = (dateInput.Date - DateTime.Now.Date).Days;
            if (dateDiff > 1)
            {
                return BadRequest("The date of the draw must not be more than one day in the past.");
            }
            else
            {
                var draw = draws.Find(d => d.DrawDate.ToString("MM-dd-yyyy") == date);
                if (draw == null)
                {
                    return BadRequest("No lottery draw for this date found!");
                }
                else
                {
                    draws.Remove(draw);
                    updateDrawTextFile();
                    return Ok(draws);
                }
            }
        }

    }
}

