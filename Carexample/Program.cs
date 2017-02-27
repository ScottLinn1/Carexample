using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Odbc;
using System.Dynamic;
using System.Runtime.InteropServices;

// add  using statement for system IO so that it we can read files
using System.IO;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Carexample
{

    // define  a  class called  Car
    internal class Car
    {
        private string make; // define a private vairable  make of type string
        private string model; // define a private vairable  model of type string
        private int year; // define a private vairable  year of type  int
        private Int32 salesFigures; // define a private vairable  salesFigures of type  int

        //Default Constructor
        public Car(string make, string model, int year, int salesFigures)
        {
            this.make = make;
            this.model = model;
            this.year = year;
            this.salesFigures = salesFigures;
        }


        // Constructor using strings
        public Car(string make, string model, string year, string salesFigures)
        {
            this.make = make;
            this.model = model;
            this.year = Convert.ToInt16(year); // convert string to integer
            this.salesFigures =  Convert.ToInt32(salesFigures); // convert string to integer
        }



        // overside the to string methdo to reurn the string speicfied.
        public override string ToString()
        {
            return  this.make + " " + this.model+  " made in the year " + this.year + " it sold " + this.salesFigures + " models in its first year"; // return  a string as set out
        }


        // create getters  and setters for  elements in the class
        public int Year
        {
            get { return year; }

            set { this.year = value; }
        }


        public string Make
        {
            get { return make; }

            set { this.make = value; }
        }



        public int SalesFigures
        {
            get { return salesFigures; }
            set { this.salesFigures = value; }
        }

        public string Model
        {
            get { return model; }
            set { this.model = value; }
        }
    }



    internal class Program
    {
        public static void Main(string[] args)
        {
            List<Car> myCarList;
            myCarList = new List<Car>();

            //try block  to catch any  issues with reading  the file
            try
            {
                using (StreamReader sr = new StreamReader("cars.csv"))
                {
                    // Read  a line from  the  file. until the  end of the file is reached.
                    while (!sr.EndOfStream)
                    {
                        var line = sr.ReadLine();
                        //write line to the console
                        Console.WriteLine(line);
                        //split the  sting using the comma character
                        string[] elements = line.Split(',');
                        //create a new Car
                        myCarList.Add(new Car(elements[0], elements[1], elements[2], elements[3]));// add the car to  list of cars.
                    }


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

           // Get index of Earliest cast
           var earliestCarIndex = GetEarliestCar(myCarList);

           //write to console
           Console.Write("The Earliest Car Model is ");

           Console.WriteLine(myCarList[earliestCarIndex].ToString());

            // Get index of largest sales
            var maxSalesIndex = GetMaxSales(myCarList);

            //write to console
            Console.Write("The Car Model with the largest sales is ");

            Console.WriteLine(myCarList[maxSalesIndex].ToString());

            // now  write the result to a file
            try
            {
                // using  streamwriter to write to a file and  define the file it  has to write to
                using (StreamWriter file = new StreamWriter("CarResults.txt"))
                {
                    file.WriteLine(myCarList[earliestCarIndex].ToString()); //write  earliest car to file
                    file.WriteLine(myCarList[maxSalesIndex].ToString()); //write  hiesht sale to  file
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }




        }


        //  method to return the index  of the car with the largest sales
        private static  int GetMaxSales(List<Car> carList)
        {
            int index = 0;
            int currentIndex = 0;
            int max = 0;

            foreach (var car in carList)
            {
                if (car.SalesFigures >max)  //check to so if value is higher
                {
                    max = car.SalesFigures; //maximum to new higher value
                    index = currentIndex;  //set index to current index value
                }
                currentIndex++; // increment  index

            }

            return index; // return index value
        }

        //  method to return the index  of the car which was manufacuted  first
        private static  int GetEarliestCar(List<Car> carList)
        {
            int index = 0;
            int currentIndex = 0;
            int min = 2000;

            foreach (var car in carList)
            {
                if (car.Year < min)   //check to so if value is low
                {
                    min = car.Year;  //minimum to new lower value
                    index = currentIndex;  //set index to current index value
                }
                currentIndex++;  // increment  index

            }

            return index; // return index value
        }
    }
}