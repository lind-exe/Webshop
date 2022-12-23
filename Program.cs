using System.Net.Http.Headers;
using Webshop.Methods;

namespace Webshop
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HardCodedValues.InsertGenres();
        }


        //          Check if data already exist in database
                    
        //            foreach (string appearence in character.Appearances)
        //            {
        //                Appearence app = appearenceList.ToList().SingleOrDefault(a => a.Name == appearence);
        //                if (app == null)
        //                {
        //                    app = new Appearence()
        //                    {
        //                        Name = appearence
        //                    };
        //                    appearenceList.Add(app);
        //                }
        //                newChar.Appearences.Add(app);
        //            }
    }
}