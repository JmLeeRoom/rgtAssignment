using POSDummySystem.Services;

namespace POSDummySystem
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Dummy POS System is starting...");

            string outputPath = "D:\\Orders"; // JSON 저장 경로
            var orderGenerator = new OrderGenerator();

            // 주문 생성 시작
            var generateTask = orderGenerator.StartGeneratingOrders(outputPath);

            // JSON 파일 읽기
            while (true)
            {
                Console.WriteLine("Loading orders from JSON...");
                try
                {
                    var orders = orderGenerator.LoadOrdersFromJson(outputPath);

                    foreach (var order in orders)
                    {
                        Console.WriteLine($"Loaded Order: {order.OrderId}, Table: {order.TableNumber}, Total: {order.TotalPrice:C}");
                    }
                }
                catch (DirectoryNotFoundException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected error: {ex.Message}");
                }

                // 1분 대기 후 다시 읽기
                await Task.Delay(TimeSpan.FromMinutes(1));
            }

            Console.WriteLine("Dummy POS System is running. Press Enter to exit.");
            Console.ReadLine();
        }
    }
}
