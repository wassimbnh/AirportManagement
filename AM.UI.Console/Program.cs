// See https://aka.ms/new-console-template for more information

using AM.ApplicationCore.Domain;
using AM.ApplicationCore.Services;
using AM.Infrastructure;

Console.WriteLine("Hello, World!");
Plane plane1= new Plane();
plane1.Capacity= 100;
plane1.ManufactureDate= DateTime.Now;
plane1.PlaneType = PlaneType.Boing;
Console.WriteLine(plane1);
//Plane plane2= new Plane(200,new DateTime(2023,02,01,11,30,05),PlaneType.Airbus);
//Object initializer
    Plane plane2= new Plane 
    { 
        Capacity=200,
        ManufactureDate= new DateTime(2023, 02, 01, 11, 30, 05) 
    };
Console.WriteLine(plane2);
Passenger pass1= new Passenger
                {FullName=new FullName
                {
                    FirstName = "aaa",
                    LastName = "bbb"
                }, 
    EmailAddress="aaa.bbb@gmail.com" };
Console.WriteLine(pass1.CheckProfile("aaa","bbb"));
Console.WriteLine(pass1.CheckProfile("aaa", "bbb","@gmail"));
pass1.PassengerType();
Staff s1 = new Staff();
Traveller t1 = new Traveller();
s1.PassengerType();
t1.PassengerType();
Console.WriteLine("**********************TP2****************");
FlightMethods fm= new FlightMethods();
fm.Flights = TestData.listFlights;
Console.WriteLine("****GetFlightDates****");
foreach (DateTime d in fm.GetFlightDates("Paris"))
Console.WriteLine(d);
Console.WriteLine("****GetFlights****");
fm.GetFlights("EstimatedDuration", "105");
Console.WriteLine("********ShowFlightDetails");
//fm.ShowFlightDetails(TestData.BoingPlane);
fm.FlightDetailsDel(TestData.BoingPlane);
Console.WriteLine("**********DurationAverage");
Console.WriteLine(fm.DurationAverage("Paris"));
Console.WriteLine("********OrderedDurationFlights");
foreach(Flight f in fm.OrderedDurationFlights())
    Console.WriteLine(f);
Console.WriteLine("************DestinationGroupedFlights");
fm.DestinationGroupedFlights();
Console.WriteLine("************Méthode d'extension");
pass1.UpperFullName();
Console.WriteLine(pass1.FullName.FirstName+" "+pass1.FullName.LastName);
//Insertion dans la BD
AMContext ctx = new AMContext();
UnitOfWork uow = new UnitOfWork(ctx);

//ctx.Planes.Add(TestData.BoingPlane);
//ctx.Planes.Add(TestData.Airbusplane);
ServicePlane sp = new ServicePlane(uow);
sp.Add(TestData.BoingPlane);
sp.Add(TestData.Airbusplane);
//ctx.Flights.Add(TestData.flight1);
ServiceFlight sf = new ServiceFlight(uow);
sf.Add(TestData.flight1);
//ctx.SaveChanges();
sp.Commit();
//Afficher le contenu de la table Flights
foreach (Flight f in ctx.Flights)
    Console.WriteLine("Flight Date: "+f.FlightDate+"Date Fabrication d'avion:"+f.Plane.ManufactureDate );

