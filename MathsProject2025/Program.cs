using System;
using System.Collections.Generic;

namespace MathProject2025
{
    class Program
    {
        // Function to display the menu
        static void ShowMenu()
        {
            Console.WriteLine("\n==========================================");
            Console.WriteLine("         DISCRETE MATHEMATICS MENU        ");
            Console.WriteLine("==========================================");
            Console.WriteLine("1. Prime Factorization");
            Console.WriteLine("2. Division Algorithm");
            Console.WriteLine("3. Extended Euclidean Algorithm");
            Console.WriteLine("4. 3x3 Matrix Inversion");
            Console.WriteLine("5. Exit");
            Console.WriteLine("==========================================");
            Console.Write("Enter your choice: ");
        }

        // Program 1: Prime Factorization
        static void PrimeFactors()
        {
            Console.WriteLine("\n--- PRIME FACTORIZATION ---");
            Console.Write("Enter a natural number (n > 1): ");
            long n = long.Parse(Console.ReadLine());

            if (n <= 1)
            {
                Console.WriteLine("Error: Number must be greater than 1.");
                return;
            }

            Console.Write($"Prime factors of {n}: ");

            long k = 2;

            while (k * k <= n)
            {
                while (n % k == 0)
                {
                    Console.Write($"{k} ");
                    n = n / k;
                }
                k++;
            }

            if (n > 1)
            {
                Console.Write(n);
            }

            Console.WriteLine();
        }

        // Program 2: Division Algorithm
        static void DivisionAlgo()
        {
            Console.WriteLine("\n--- DIVISION ALGORITHM ---");
            Console.Write("Enter dividend (a): ");
            long a = long.Parse(Console.ReadLine());
            Console.Write("Enter divisor (b, non-zero): ");
            long b = long.Parse(Console.ReadLine());

            if (b == 0)
            {
                Console.WriteLine("Error: Divisor cannot be zero.");
                return;
            }

            long q, r;

            if (b > 0)
            {
                q = (long)Math.Floor((double)a / (double)b);
            }
            else
            {
                q = (long)Math.Ceiling((double)a / (double)b);
            }

            r = a - b * q;

            Console.WriteLine("\nResults:");
            Console.WriteLine($"a = {a}, b = {b}");
            Console.WriteLine($"Quotient (q) = {q}");
            Console.WriteLine($"Remainder (r) = {r}");
            Console.WriteLine($"Verification: {a} = {b} * {q} + {r}");
        }

        // Program 3: Extended Euclidean Algorithm
        static void ExtendedEuclidean()
        {
            Console.WriteLine("\n--- EXTENDED EUCLIDEAN ALGORITHM ---");
            Console.WriteLine("Choose suboption:");
            Console.WriteLine("1. Compute gcd(a,b) and coefficients");
            Console.WriteLine("2. Find RSA decryption key");
            Console.Write("Enter choice (1 or 2): ");

            string subchoice = Console.ReadLine();

            if (subchoice == "1")
            {
                Console.Write("\nEnter first positive integer (a >= b): ");
                long a = long.Parse(Console.ReadLine());
                Console.Write("Enter second positive integer (b): ");
                long b = long.Parse(Console.ReadLine());

                if (a < b || a <= 0 || b <= 0)
                {
                    Console.WriteLine("Error: Both numbers must be positive and a >= b.");
                    return;
                }

                // Initialize U = [a, 1, 0] and V = [b, 0, 1]
                List<long> U = new List<long> { a, 1, 0 };
                List<long> V = new List<long> { b, 0, 1 };
                List<long> W = new List<long> { 0, 0, 0 };

                while (V[0] > 0)
                {
                    long quotient = (long)Math.Floor((double)U[0] / (double)V[0]);

                    for (int i = 0; i < 3; i++)
                    {
                        W[i] = U[i] - quotient * V[i];
                    }

                    // Update U = V
                    U = new List<long>(V);
                    // Update V = W
                    V = new List<long>(W);
                }

                long d = U[0]; // This is our gcd                                                      
                long x = U[1]; // Coefficient for a
                long y = U[2]; // Coefficient for b

                Console.WriteLine("\nResults:");
                Console.WriteLine($"gcd({a}, {b}) = {d}");
                Console.WriteLine($"Coefficients: x = {x}, y = {y}");
                Console.WriteLine($"Verification: {d} = {a} * {x} + {b} * {y}");
                Console.WriteLine($"Check: {(a * x + b * y)}");
            }
            else if (subchoice == "2")
            {
                // RSA decryption key calculation with the predefined values
                long p = 701, q = 1051, e = 487;
                long n = p * q;
                long phi = (p - 1) * (q - 1);

                Console.WriteLine("\nRSA Parameters:");
                Console.WriteLine($"p = {p}, q = {q}");
                Console.WriteLine($"n = p * q = {n}");
                Console.WriteLine($"e = {e}");
                Console.WriteLine($"φ(n) = (p-1)(q-1) = {phi}");

                // Find the decryption key d where (e × d) divided by phi gives remainder 1
                // This means we need to find numbers d and y that satisfy: e × d + phi × y = 1
                List<long> U = new List<long> { e, 1, 0 };
                List<long> V = new List<long> { phi, 0, 1 };
                List<long> W = new List<long> { 0, 0, 0 };

                while (V[0] > 0)
                {
                    long qRSA = (long)Math.Floor((double)U[0] / (double)V[0]);
                    for (int i = 0; i < 3; i++)
                    {
                        W[i] = U[i] - qRSA * V[i];
                    }
                    U = new List<long>(V);
                    V = new List<long>(W);
                }

                long d = U[1]; // coefficient for e
                if (d < 0) d += phi; // Make positive

                Console.WriteLine($"\nDecryption key d = {d}");
                Console.WriteLine($"Verification: (e * d) mod φ(n) = {((e * d) % phi)} (should be 1)");
            }
            else
            {
                Console.WriteLine("Invalid choice.");
            }
        }

        // Program 4: Matrix Inversion
        static void MatrixInverse()
        {
            double[,] A = new double[3, 3];
            Console.WriteLine("\n--- 3x3 MATRIX INVERSION ---");
            Console.WriteLine("Enter the 9 elements of the 3x3 matrix (row by row):");

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write($"A[{i + 1}][{j + 1}]: ");
                    A[i, j] = double.Parse(Console.ReadLine());
                }
            }

            // Calculate determinant
            double det = A[0, 0] * A[1, 1] * A[2, 2] +
                         A[0, 1] * A[1, 2] * A[2, 0] +
                         A[0, 2] * A[1, 0] * A[2, 1] -
                         A[0, 2] * A[1, 1] * A[2, 0] -
                         A[0, 0] * A[1, 2] * A[2, 1] -
                         A[0, 1] * A[1, 0] * A[2, 2];

            Console.WriteLine($"\nDeterminant |A| = {det:F3}");

            if (det == 0)
            {
                Console.WriteLine("ERROR: the matrix is not invertible.");
                return;
            }

            // Calculate inverse matrix components
            double[,] invA = new double[3, 3];

            invA[0, 0] = (A[1, 1] * A[2, 2] - A[1, 2] * A[2, 1]) / det;
            invA[0, 1] = (A[0, 2] * A[2, 1] - A[0, 1] * A[2, 2]) / det;
            invA[0, 2] = (A[0, 1] * A[1, 2] - A[0, 2] * A[1, 1]) / det;

            invA[1, 0] = (A[1, 2] * A[2, 0] - A[1, 0] * A[2, 2]) / det;
            invA[1, 1] = (A[0, 0] * A[2, 2] - A[0, 2] * A[2, 0]) / det;
            invA[1, 2] = (A[0, 2] * A[1, 0] - A[0, 0] * A[1, 2]) / det;

            invA[2, 0] = (A[1, 0] * A[2, 1] - A[1, 1] * A[2, 0]) / det;
            invA[2, 1] = (A[0, 1] * A[2, 0] - A[0, 0] * A[2, 1]) / det;
            invA[2, 2] = (A[0, 0] * A[1, 1] - A[0, 1] * A[1, 0]) / det;

            Console.WriteLine("\nInverse matrix A^-1:");
            for (int i = 0; i < 3; i++)
            {
                Console.Write("[ ");
                for (int j = 0; j < 3; j++)
                {
                    Console.Write($"{invA[i, j],10:F3} ");
                }
                Console.WriteLine("]");
            }
        }

        // Main function
        static void Main(string[] args)
        {
            Console.WriteLine("========== SD2 MAIN PROJECT 2025 =========");
            Console.WriteLine("Discrete Mathematics ~ Oliver Hyde");

            string choice;

            while (true)
            {
                ShowMenu();
                choice = Console.ReadLine();

                if (choice == "1") PrimeFactors();
                else if (choice == "2") DivisionAlgo();
                else if (choice == "3") ExtendedEuclidean();
                else if (choice == "4") MatrixInverse();
                else if (choice == "5") break;
                else Console.WriteLine("Invalid option. Try again.");

                if (choice != "5")
                {
                    Console.WriteLine("\nPress ENTER to return to menu...");
                    Console.ReadLine();
                }
            }

            Console.WriteLine("Goodbye!");
        }
    }
}