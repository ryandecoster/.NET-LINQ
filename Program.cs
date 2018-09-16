using System;
using System.Collections.Generic;
using System.Linq;
using JsonData;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Collections to work with
            List<Artist> Artists = JsonToFile<Artist>.ReadJson();
            List<Group> Groups = JsonToFile<Group>.ReadJson();
            System.Console.WriteLine("Hello!");
            
            //There is only one artist in this collection from Mount Vernon, what is their name and age?
            var artist = Artists.Where(a => a.Hometown == "Mount Vernon");
            foreach (var item in artist) {
                System.Console.WriteLine(item.RealName);
                System.Console.WriteLine(item.Age);
            }

            //Who is the youngest artist in our collection of artists?
            var art = Artists.OrderByDescending(a => a.Age).Last();
            System.Console.WriteLine(art.ArtistName);
            System.Console.WriteLine(art.Age);

            //Display all artists with 'William' somewhere in their real name
            var william = Artists.Where(a => a.RealName.Contains("William"));
            foreach (var i in william) {
                System.Console.WriteLine(i.RealName);
            }

            //Display the 3 oldest artist from Atlanta
            var old = Artists.Where(a => a.Hometown == "Atlanta").OrderByDescending(a => a.Age).Take(3);
            foreach(var i in old) {
                System.Console.WriteLine(i.ArtistName);
            }
            
            //(Optional) Display the Group Name of all groups that have members that are not from New York City
            var query =
                (from g in Groups
                join a in Artists.Where(a => a.Hometown != "New York City")
                    on g.Id equals a.GroupId
                select new {g.GroupName}).Distinct();
                foreach (var i in query) {
                    System.Console.WriteLine(i.GroupName + " has no members from NYC.");
                }

            //(Optional) Display the artist names of all members of the group 'Wu-Tang Clan'
            var wutang =
                from g in Groups.Where(g => g.GroupName == "Wu-Tang Clan")
                join a in Artists
                    on g.Id equals a.GroupId
                select new {a.ArtistName};
            foreach (var i in wutang) {
                System.Console.WriteLine(i.ArtistName + " is in the Wu-Tang Clan.");
            }
        }
    }
}
