using CalculatorProxyService;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    enum Operation
    {
        Additiona = 1,
        Subtraction,
        Multiply,
        Division,
        Square,
        JournalQuery
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Calculator Service");
            Console.Write("Enter your identificator: ");
            var inputIdIsCorrect = int.TryParse(Console.ReadLine(), out var id);
            Console.WriteLine("The operations allowed are: ");
            var counter = 1;
            foreach (var value in Enum.GetValues(typeof(Operation)))
            {
                Console.WriteLine($"{counter}. {value}");
                counter++;
            }
            var exitCommand = string.Empty;
            do
            {
                Console.Write("Enter the number operation: ");
                try
                {
                    var inputOptionIsCorrect = Enum.TryParse<Operation>(Console.ReadLine(), out var operation);
                    if (!inputIdIsCorrect | id == byte.MinValue)
                    {
                        throw new InvalidOperationException("The id must be a number or different of zero");
                    }
                    if (!inputOptionIsCorrect)
                    {
                        throw new InvalidOperationException("The option selected is not valid, please check again the list and select a number option");
                    }
                    var proxy = new Proxy(id);
                    switch (operation)
                    {
                        case Operation.Additiona:
                            Add(proxy);
                            break;
                        case Operation.Subtraction:
                            Sub(proxy);
                            break;
                        case Operation.Multiply:
                            Mult(proxy);
                            break;
                        case Operation.Division:
                            Div(proxy);
                            break;
                        case Operation.Square:
                            Sqrt(proxy);
                            break;
                        case Operation.JournalQuery:
                            JournalQuery(proxy);
                            break;
                    }
                }
                catch (InvalidOperationException ioex)
                {
                    Console.WriteLine(ioex.Message);
                    Console.Write("Enter your identificator: ");
                    inputIdIsCorrect = int.TryParse(Console.ReadLine(), out id);
                    continue;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException.Message);
                    continue;
                }
                Console.Write("Press Y to exit: ");
                exitCommand = Console.ReadLine();
            } while (exitCommand.ToUpper() != "Y");
            Console.WriteLine("Press <enter> to exit");
            Console.ReadLine();
        }

        static void Add(Proxy proxy)
        {
            Console.Write("Enter the number to addition, separated by coma: ");
            var values = Console.ReadLine();
            var additional = new Additional
            {
                Addends = values.Split(',').Select(v =>
                {
                    if (!int.TryParse(v, out var output))
                    {
                        return default(int?);
                    }
                    else
                    {
                        return output;
                    }
                }).ToArray()
            };
            var result = proxy.Add(additional);
            Console.WriteLine($"The result operation is: {result.ToString()}");
        }

        static void Sub(Proxy proxy)
        {
            Console.Write("Enter the minuend number and separated by coma the subtrahend number: ");
            var values = Console.ReadLine();
            var numbers = values.Split(',').Select(v =>
            {
                if (!int.TryParse(v, out var output))
                {
                    return default(int?);
                }
                else
                {
                    return output;
                }
            }).ToArray();
            var subtraction = new Substraction
            {
                Minuend = numbers.ElementAtOrDefault(0),
                Subtrahend = numbers.ElementAtOrDefault(1)
            };
            var result = proxy.Sub(subtraction);
            Console.WriteLine($"The result operation is: {result.ToString()}");
        }

        static void Mult(Proxy proxy)
        {
            Console.Write("Enter the number to multiply, separated by coma: ");
            var values = Console.ReadLine();
            var multiply = new Multiply
            {
                Factors = values.Split(',').Select(v =>
                {
                    if (!int.TryParse(v, out var output))
                    {
                        return default(int?);
                    }
                    else
                    {
                        return output;
                    }
                }).ToArray()
            };
            var result = proxy.Mult(multiply);
            Console.WriteLine($"The result operation is: {result.ToString()}");
        }

        static void Div(Proxy proxy)
        {
            Console.Write("Enter the dividend number and separated by coma the divisor number: ");
            var values = Console.ReadLine();
            var numbers = values.Split(',').Select(v =>
            {
                if (!int.TryParse(v, out var output))
                {
                    return default(int?);
                }
                else
                {
                    return output;
                }
            }).ToArray();
            var division = new Division
            {
                Dividend = numbers.ElementAtOrDefault(0),
                Divisor = numbers.ElementAtOrDefault(1)
            };
            var result = proxy.Div(division);
            Console.WriteLine($"The result operation is: {result.ToString()}");
        }

        static void Sqrt(Proxy proxy)
        {
            Console.Write("Enter the number to calculate the value square: ");
            var value = Console.ReadLine();
            var number = default(int?);
            if (int.TryParse(value, out var output))
            {
                number = output;
            }
            var square = new Square
            {
                Number = number
            };
            var result = proxy.Sqrt(square);
            Console.WriteLine($"The result operation is: {result.ToString()}");
        }

        static void JournalQuery(Proxy proxy)
        {
            Console.Write("Enter ID to search: ");
            var value = Console.ReadLine();
            var id = default(int?);
            if (int.TryParse(value, out var output))
            {
                id = output;
            }
            var journalQuery = new JournalQuery
            {
                Id = id
            };
            var result = proxy.JournalQuery(journalQuery);
            if (!result.Operations.Any())
            {
                Console.WriteLine($"There aren't operatios for id: {journalQuery.Id}");
            }
            else
            {
                Console.WriteLine("The operations are: ");
                foreach (var operation in result.Operations)
                {
                    Console.WriteLine(operation.ToString());
                }
            }
        }
    }
}
