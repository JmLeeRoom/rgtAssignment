using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OrderService.Models;


namespace OrderService.Services
{
    public class OrderProcessor
    {
        public void ProcessOrders(string inputPath, string outputPath)
        {
            var files = Directory.GetFiles(inputPath, "*.json");

            foreach (var file in files)
            {
                try
                {
                    // JSON 파일 읽기
                    var json = File.ReadAllText(file);
                    var order = JsonConvert.DeserializeObject<Order>(json);

                    // 주문 상태 변경 (예: 처리 완료로 업데이트)
                    order.Status = "Completed";

                    // 결과를 출력 경로로 저장
                    var outputFilePath = Path.Combine(outputPath, Path.GetFileName(file));
                    File.WriteAllText(outputFilePath, JsonConvert.SerializeObject(order, Formatting.Indented));

                    // 원본 파일 삭제
                    File.Delete(file);

                    Console.WriteLine($"Processed Order: {order.OrderId}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing file {file}: {ex.Message}");
                }
            }
        }
    }
}
