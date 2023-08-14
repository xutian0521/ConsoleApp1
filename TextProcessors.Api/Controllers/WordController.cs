using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TextProcessors.Api.Models;

namespace TextProcessors.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WordController: ControllerBase
    {

        [HttpPost("WordStoryAnalysis")]
        public IActionResult WordStoryAnalysis(StoryModel model)
        {
            string v = this.Request.Query["isAppendCount"];
            bool isAppendCount = v == "1"? true: false;
            v_WordStoryAnalysis r = new v_WordStoryAnalysis();
            Dictionary<string, int> worddictBold = new Dictionary<string, int>();
            Dictionary<string, int> worddictNormal = new Dictionary<string, int>();
            string wordItem = "";
            foreach (var item in model.Word.Split("\r\n"))
            {
                wordItem = item.Trim();
                if (model.Story.Contains(wordItem) 
                    && 
                        (
                            //(model.Story.Contains(wordItem + " ") || model.Story.Contains(" " + wordItem)) 
                            //||
                            (model.Story.Contains(wordItem + "**") || model.Story.Contains("**" + wordItem)) 
                        )
                    )
                {
                    int c = CountOccurrencesIgnoreCase(model.Story, wordItem);
                    worddictBold.Add(wordItem, c);
                    r.WordAndCount += wordItem + "," + c + "\r\n";
                    r.WordAppeared += wordItem + "\r\n";
                }
                else
                {
                    if (model.Story.Contains(wordItem + " ") || model.Story.Contains(" " + wordItem))
                    {
                        worddictBold.Add(wordItem, -1);
                        r.WordAndCount += wordItem + "," + -1 + "\r\n";
                        r.WordNotAppear += wordItem + "\r\n";
                    }
                    else
                    {
                        worddictBold.Add(wordItem, 0);
                        r.WordAndCount += wordItem + "," + 0 + "\r\n";
                        r.WordNotAppear += wordItem + "\r\n";
                    }

                }
                
                
            }
            var responseText = "";
            string responseTextBefore = "";
            string responseTextMiddle = "";
            string responseTextAfter = "";
            foreach (var item in worddictBold)
            {
                string endStr = "";
                if (item.Value > 0)
                {
                    endStr = "," + item.Value;
                    if (!isAppendCount)
                    {
                        endStr = "";
                    }
                    responseTextBefore += item.Key + endStr + "\r\n";
                }
                if (item.Value == 0)
                {
                    responseTextMiddle += item.Key + "\r\n";
                }
                if (item.Value < 0)
                {
                    endStr = "  [NotBold]";
                    if (!isAppendCount)
                    {
                        endStr = "";
                    }
                    responseTextAfter += item.Key + endStr + "\r\n";
                }
                
            }
            responseText = responseTextBefore + "--- \r\n" + responseTextMiddle + "--- \r\n" + responseTextAfter;
            return new ContentResult
            {
                Content = responseText,
                ContentType = "text/plain",
                StatusCode = (int)HttpStatusCode.OK
            };
        }

        public int CountOccurrencesIgnoreCase(string text, string pattern)
        {
            int count = 0;
            int startIndex = 0;

            while ((startIndex = text.IndexOf(pattern, startIndex, StringComparison.OrdinalIgnoreCase)) != -1)
            {
                count++;
                startIndex += pattern.Length;
            }

            return count;
        }

    }
}
