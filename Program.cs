using System.IO;

string[] productNames = new string[100];
int[] productQuantities = new int[100];
int itemCount = 0;
bool isRunning = true;

string filePath = "inventory.txt";

if (File.Exists(filePath))
{
    string[] savedLines = File.ReadAllLines(filePath);

    for (int i = 0; i < savedLines.Length; i++)
    {
        
        string[] parts = savedLines[i].Split(',');

        productNames[itemCount] = parts[0];
        productQuantities[itemCount] = int.Parse(parts[1]);
        itemCount++; 
    }
}

while (isRunning) {
    Console.Clear();
    Console.WriteLine("=====INVENTORY MANAGER======");
    Console.WriteLine("1. Add a new product");
    Console.WriteLine("2. View all products");
    Console.WriteLine("3. Delete a product");
    Console.WriteLine("4. Exit");
    Console.WriteLine("\nChoose an option:");

    string choice = Console.ReadLine();

    switch (choice) {
        case "1":
            Console.WriteLine("\n--- ADD NEW PRODUCT ---");
         
            if (itemCount < 100)
            {
                Console.Write("Enter product name: ");
                string name = Console.ReadLine();

                Console.Write("Enter quantity: ");
                int quantity; 
                while (!int.TryParse(Console.ReadLine(), out quantity) || quantity < 0)
                {
                    Console.WriteLine("Invalid input. You must enter a whole number (0 or higher).");
                    Console.Write("Enter quantity: ");
                }

                productNames[itemCount] = name;
                productQuantities[itemCount] = quantity;

                itemCount++;

                string[] linesToSave = new string[itemCount];

                for (int i = 0; i < itemCount; i++)
                {
          
                    linesToSave[i] = $"{productNames[i]},{productQuantities[i]}";
                }

                File.WriteAllLines(filePath, linesToSave);

                Console.WriteLine($"\nSuccess! Added {name} to the inventory.");

                Console.WriteLine($"\nAdded {name} to the inventory.");
            }
            else
            {
                Console.WriteLine("\nInventory is full! Cannot add more items.");
            }

            Console.ReadLine(); 
            break;

        case "2":
            Console.WriteLine("\n--- CURRENT INVENTORY ---");
            
            if (itemCount == 0)
            {
                Console.WriteLine("The inventory is empty. Add some products first!");
            }
            else
            {

                for (int i = 0; i < itemCount; i++)
                { 
                    Console.WriteLine($"{i + 1}. {productNames[i]} - Quantity: {productQuantities[i]}");
                }
            }

            Console.WriteLine("\nPress Enter to return to the menu.");
            Console.ReadLine();
            break;

        case "3":
            Console.WriteLine("\n--- DELETE PRODUCT ---");

            if (itemCount == 0)
            {
                Console.WriteLine("The inventory is empty. Nothing to delete!");
            }
            else
            {
                
                for (int i = 0; i < itemCount; i++)
                {
                    Console.WriteLine($"{i + 1}. {productNames[i]} - Quantity: {productQuantities[i]}");
                }

                Console.Write("\nEnter the number of the item to delete: ");
                int deleteChoice;

                while (!int.TryParse(Console.ReadLine(), out deleteChoice) || deleteChoice < 1 || deleteChoice > itemCount)
                {
                    Console.WriteLine("Invalid choice. Please enter a valid number from the list.");
                    Console.Write("Enter the number of the item to delete: ");
                }

                int indexToDelete = deleteChoice - 1;
                string deletedName = productNames[indexToDelete]; 

                for (int i = indexToDelete; i < itemCount - 1; i++)
                {
                    productNames[i] = productNames[i + 1];
                    productQuantities[i] = productQuantities[i + 1];
                }

                itemCount--;

                string[] linesToSave = new string[itemCount];
                for (int i = 0; i < itemCount; i++)
                {
                    linesToSave[i] = $"{productNames[i]},{productQuantities[i]}";
                }
                File.WriteAllLines(filePath, linesToSave);

                Console.WriteLine($"\nSuccess! Deleted {deletedName} from inventory.");
            }

            Console.ReadLine();
            break;

        case "4":
            isRunning = false; 
            Console.WriteLine("\nGoodbye!");
            break;

        default: 
            Console.WriteLine("\nInvalid choice. Press Enter to try again.");
            Console.ReadLine();
            break;
    }
}