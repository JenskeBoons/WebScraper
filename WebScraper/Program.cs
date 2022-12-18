using System;
using System.Diagnostics.Metrics;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections;
using System.Collections.Specialized;
using System.Threading.Channels;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection.Metadata;
using System.Data;

namespace WebScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("choose:");
            var choose = Console.ReadLine();
            if (choose == "yt")
            {
                var csv = new StringBuilder();
                var json = new StringBuilder();
                
                Console.Write("input:");
                var input = Console.ReadLine();
                IWebDriver driver = new ChromeDriver();
                driver.Navigate().GoToUrl("https://www.youtube.com/results?search_query=" + input + "&sp=CAI%253D");
                System.Threading.Thread.Sleep(1000);
                var title = driver.FindElements(By.Id("video-title"));
                var views = driver.FindElements(By.XPath("//*[@id=\"metadata-line\"]/span[1]"));
                var date = driver.FindElements(By.XPath("//*[@id=\"metadata-line\"]/span[2]"));
                var channel = driver.FindElements(By.XPath("//*[@id=\"channel-info\"]")); 

                json.AppendLine("[");
                csv.AppendLine("channel" + "\";\"" + "title" + "\";\"" + "views" + "\";\"" + "date" + "\";\"" + "link");

                var count = 0;
                while (count < 5)
                {
                    Console.WriteLine(title[count].Text);
                    Console.WriteLine(channel[count].Text);
                    Console.WriteLine(views[count].Text);
                    Console.WriteLine(date[count].Text);
                    Console.WriteLine(title[count].GetAttribute("href"));
                    Console.WriteLine();

                    json.AppendLine("{");
                    json.AppendLine("\"title\":\"" + title[count].Text + "\",");
                    json.AppendLine("\"channel\":\"" + channel[count].Text + "\",");
                    json.AppendLine("\"views\":\"" + views[count].Text + "\",");
                    json.AppendLine("\"date\":\"" + date[count].Text + "\",");
                    json.AppendLine("\"link\":\"" + title[count].GetAttribute("href") + "\"");
                    if (count == 4)
                    {
                        json.AppendLine("}");
                    }
                    else
                    {
                        json.AppendLine("},");
                    }

                    csv.AppendLine(channel[count].Text + "\";\"" + title[count].Text + "\";\"" + views[count].Text + "\";\"" + date[count].Text + "\";\"" + title[count].GetAttribute("href"));

                    count++;
                }
                json.AppendLine("]");
                File.WriteAllText("C:\\Users\\JensB\\OneDrive\\Documenten\\School\\thomasmore\\2ITF\\DevOps\\WebScraper\\test.csv", csv.ToString());
                File.WriteAllText("C:\\Users\\JensB\\OneDrive\\Documenten\\School\\thomasmore\\2ITF\\DevOps\\WebScraper\\test.json", json.ToString());
                //driver.Quit();
            }
            else if (choose == "job")
            {
                var csv = new StringBuilder();
                var json = new StringBuilder();

                Console.Write("input:");
                var input = Console.ReadLine();
                IWebDriver driver = new ChromeDriver();
                driver.Navigate().GoToUrl("https://www.ictjob.be/nl/it-vacatures-zoeken?keywords=" + input);
                System.Threading.Thread.Sleep(10000);
                var popup = driver.FindElement(By.XPath("/html/body/div[2]/a"));
                popup.Click();
                var date = driver.FindElement(By.XPath("//*[@id=\"sort-by-date\"]"));
                date.Click();
                System.Threading.Thread.Sleep(15000);

                var title = "title";
                var company = "company";
                var location = "location";
                var keywords = "keywords";
                var link = "link";

                json.AppendLine("[");
                csv.AppendLine(title + "\";\"" + company + "\";\"" + location + "\";\"" + keywords + "\";\"" + link);

                var count = 0;
                while (count < 6)
                {
                    count++;
                    if (count == 4)
                    {
                        count++;
                    }
                    title = driver.FindElement(By.XPath("html/body/section/div[1]/div/div[2]/div/div/form/div[2]/div/div/div[2]/section/div/div[2]/div[1]/div/ul/li[" + count + "]/span[2]/a/h2")).Text;
                    company = driver.FindElement(By.XPath("html/body/section/div[1]/div/div[2]/div/div/form/div[2]/div/div/div[2]/section/div/div[2]/div[1]/div/ul/li[" + count + "]/span[2]/span[1]")).Text;
                    location = driver.FindElement(By.XPath("/html/body/section/div[1]/div/div[2]/div/div/form/div[2]/div/div/div[2]/section/div/div[2]/div[1]/div/ul/li[" + count + "]/span[2]/span[2]/span[2]/span/span")).Text;
                    keywords = driver.FindElement(By.XPath("/html/body/section/div[1]/div/div[2]/div/div/form/div[2]/div/div/div[2]/section/div/div[2]/div[1]/div/ul/li[" + count + "]/span[2]/span[3]")).Text;
                    link = driver.FindElement(By.XPath("html/body/section/div[1]/div/div[2]/div/div/form/div[2]/div/div/div[2]/section/div/div[2]/div[1]/div/ul/li[" + count + "]/span[2]/a")).GetAttribute("href");
                    Console.WriteLine(title);
                    Console.WriteLine(company);
                    Console.WriteLine(location);
                    Console.WriteLine(keywords);
                    Console.WriteLine(link);
                    Console.WriteLine();

                    json.AppendLine("{");
                    json.AppendLine("\"title\": \"" + title + "\",");
                    json.AppendLine("\"company\": \"" + company + "\",");
                    json.AppendLine("\"location\": \"" + location + "\",");
                    json.AppendLine("\"keywords\": \"" + keywords + "\",");
                    json.AppendLine("\"link\": \"" + link + "\"");
                    if (count == 6)
                    {
                        json.AppendLine("}");
                    }
                    else
                    {
                        json.AppendLine("},");
                    }
                    csv.AppendLine(title + "\";\"" + company + "\";\"" + location + "\";\"" + keywords + "\";\"" + link);
                }
                json.AppendLine("]");
                File.WriteAllText("C:\\Users\\JensB\\OneDrive\\Documenten\\School\\thomasmore\\2ITF\\DevOps\\WebScraper\\test.csv", csv.ToString());
                File.WriteAllText("C:\\Users\\JensB\\OneDrive\\Documenten\\School\\thomasmore\\2ITF\\DevOps\\WebScraper\\test.json", json.ToString());

            }

            else if (choose == "news")
            {
                var csv = new StringBuilder();
                var json = new StringBuilder();

                Console.Write("input:");
                var input = Console.ReadLine();
                IWebDriver driver = new ChromeDriver();
                driver.Navigate().GoToUrl("https://news.google.com/search?q=" + input + "&hl=nl&gl=BE&ceid=BE%3Anl");
                System.Threading.Thread.Sleep(5000);
                var popup = driver.FindElement(By.XPath("/html/body/c-wiz/div/div/div/div[2]/div[1]/div[3]/div[1]/div[1]/form[1]/div/div/button/span"));
                popup.Click();
                System.Threading.Thread.Sleep(5000);

                var paper = "paper";
                var artickle = "artickle";
                var link = "link";
                var date = "date";

                csv.AppendLine(paper + "\";\"" + artickle + "\";\"" + date + "\";\"" + link);
                json.AppendLine("[");

                var count = 1;
                while (count < 6)
                {
                    count++;
                    paper = driver.FindElement(By.XPath("/html/body/c-wiz/div/div[2]/div[2]/div/main/c-wiz/div[1]/div[" + count + "]/div/article/div[1]/a")).Text;
                    artickle = driver.FindElement(By.XPath("/html/body/c-wiz/div/div[2]/div[2]/div/main/c-wiz/div[1]/div[" + count + "]/div/article/h3/a")).Text;
                    link = driver.FindElement(By.XPath("/html/body/c-wiz/div/div[2]/div[2]/div/main/c-wiz/div[1]/div[" + count + "]/div/article/h3/a")).GetAttribute("href");
                    date = driver.FindElement(By.XPath("/html/body/c-wiz/div/div[2]/div[2]/div/main/c-wiz/div[1]/div[" + count + "]/div/article/div[2]/div")).Text;
                    Console.WriteLine(paper);
                    Console.WriteLine(artickle);
                    Console.WriteLine(date);
                    Console.WriteLine(link);
                    Console.WriteLine();

                    json.AppendLine("{");
                    json.AppendLine("\"paper\": \"" + paper + "\",");
                    json.AppendLine("\"artickle\": \"" + artickle + "\",");
                    json.AppendLine("\"date\": \"" + date + "\",");
                    json.AppendLine("\"link\": \"" + link + "\"");
                    if (count == 6)
                    {
                        json.AppendLine("}");
                    }
                    else
                    {
                        json.AppendLine("},");
                    }
                    csv.AppendLine(paper + "\";\"" + artickle + "\";\"" + date + "\";\"" + link); 
                }
                json.AppendLine("]");
                File.WriteAllText("C:\\Users\\JensB\\OneDrive\\Documenten\\School\\thomasmore\\2ITF\\DevOps\\WebScraper\\test.json", json.ToString());
                File.WriteAllText("C:\\Users\\JensB\\OneDrive\\Documenten\\School\\thomasmore\\2ITF\\DevOps\\WebScraper\\test.csv", csv.ToString());
                //driver.Quit();
            }
        }
    }
}
