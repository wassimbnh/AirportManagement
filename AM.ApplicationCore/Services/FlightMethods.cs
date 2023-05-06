using AM.ApplicationCore.Domain;
using AM.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Plane = AM.ApplicationCore.Domain.Plane;

namespace AM.ApplicationCore.Services
{
    public class FlightMethods:IFlightMethods
    {
        public Action<Plane> FlightDetailsDel { get; set; }
        public Func<string,double> DurationAverageDel { get; set; }
        public FlightMethods()
        {
            // FlightDetailsDel = ShowFlightDetails;
            FlightDetailsDel =
                pl =>
                {
                    var req = from f in Flights
                              where f.Plane == pl
                              select new { f.Destination, f.FlightDate };
                    foreach (var a in req)
                        Console.WriteLine(a);
                };
            // DurationAverageDel = DurationAverage;
            DurationAverageDel = dest =>
            {
                var req = from f in Flights
                          where f.Destination == dest
                          select f.EstimatedDuration;
                return req.Average();
            };
        }
        public List<Flight> Flights { get; set; }= new List<Flight>();

        public IEnumerable<IGrouping<string, Flight>> DestinationGroupedFlights()
        {
            //var req = from f in Flights
            //          group f by f.Destination;
            var req = Flights.GroupBy(f => f.Destination);
            foreach (var g in req)
            {
                Console.WriteLine(g.Key);
                foreach(Flight f in g)
                Console.WriteLine(f);

            }
            return req;
        }

        public double DurationAverage(string destination)
        {
            //var req = from f in Flights
            //          where f.Destination == destination
            //          select f.EstimatedDuration;
            return Flights.Where(f=> f.Destination == destination)
                .Average(f => f.EstimatedDuration);
            
        }

        public List<DateTime> GetFlightDates(string destination)
        {
            //List<DateTime> ls = new List<DateTime>();
            //foreach(Flight flight in Flights)
            //{
            //    if(flight.Destination.Equals(destination))
            //        ls.Add(flight.FlightDate);
            //}
            //List<DateTime> ls = (from f in Flights
            //                    where f.Destination == destination
            //                    select f.FlightDate).ToList();
           
            //return ls;
            return Flights.Where(f=> f.Destination == destination)
                .Select(f=>f.FlightDate).ToList();
        }

        public void GetFlights(string filterType, string filterValue)
        {
            switch(filterType) {
                case "Destination":
                    foreach (var f in Flights)
                        if(f.Destination.Equals(filterValue))
                            Console.WriteLine(f);
                break;
                case "Departure":
                    foreach (var f in Flights)
                        if (f.Departure.Equals(filterValue))
                            Console.WriteLine(f);
                    break;
                case "FlightDate":
                    foreach (var f in Flights)
                        if (f.FlightDate.Equals(DateTime.Parse(filterValue)))
                            Console.WriteLine(f);
                    break;
                case "EstimatedDuration":
                    foreach (var f in Flights)
                        if (f.EstimatedDuration.Equals(int.Parse(filterValue)))
                            Console.WriteLine(f);
                    break;
                    default: Console.WriteLine("Incorrect filter type");
                        break;

            }
        }

        public IEnumerable<Flight> OrderedDurationFlights()
        {
            //    var req = from f in Flights
            //              orderby f.EstimatedDuration descending
            //              select f;
            //    return req;
            return Flights.OrderByDescending(f => f.EstimatedDuration);
        }

        public int ProgrammedFlightNumber(DateTime startDate)
        {
            //var req=from f in Flights
            //        where (f.FlightDate-startDate).TotalDays<7
            //        && (f.FlightDate - startDate).TotalDays>0
            //        select f;
            //  return req.Count();
            return Flights
                .Where(f => (f.FlightDate - startDate).TotalDays < 7
                && (f.FlightDate - startDate).TotalDays > 0).Count();
        }

        public IEnumerable<Traveller> SeniorTravellers(Flight flight)
        {
            //var req = from t in flight.Passengers.OfType<Traveller>()
            //          orderby t.BirthDate
            //          select t;
            //return req.Take(3);
            //return flight.Passengers.OfType<Traveller>()
            //    .OrderBy(t => t.BirthDate).Take(3);
            //req.Skip(3) pour ignorer 3
            return null;
        }

        public void ShowFlightDetails(Plane plane)
        {
            //var req = from f in Flights
            //          where f.Plane == plane
            //          select new { f.Destination, f.FlightDate };
            var req = Flights.Where(f=>f.Plane==plane)
                .Select(f=> new { f.Destination, f.FlightDate });
             foreach(var a in req)
                Console.WriteLine(a);       
        }
    }
}
