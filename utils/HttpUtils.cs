using System.Text;

namespace HelloJniLib.utils;

public class HttpUtils {
        
    public static async Task<string> get(string url) {
        using HttpClient httpClient = new HttpClient();
        HttpResponseMessage response = await httpClient.GetAsync(url);
        // // 准备 POST 数据
        // var postData = new { key1 = "value1", key2 = "value2" };
        // var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(postData), Encoding.UTF8, "application/json");
        // 发送 POST 请求
        // HttpResponseMessage response = await httpClient.PostAsync("https://api.example.com/post-endpoint", content);
        // 处理响应
        if (response.IsSuccessStatusCode) {
            string rsp = await response.Content.ReadAsStringAsync();
            Log.d($"HTTPGET code: {response.StatusCode}, rsp:{rsp}");
            return rsp;
        } else {
            Log.w($"HTTPGET Error: {response.StatusCode}");
            return null;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// var postData = new { key1 = "value1", key2 = "value2" };
    /// var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(postData), Encoding.UTF8, "application/json");
    /// <param name="url"></param>
    /// <param name="jsonstr"></param>
    /// <returns></returns>
    
    public static async Task<string> post(string url,string jsonstr) {
        using HttpClient httpClient = new HttpClient();
        // 发送 POST 请求
        HttpResponseMessage response = await httpClient.PostAsync(url,new StringContent(jsonstr,Encoding.UTF8,"application/json"));
        // 处理响应
        if (response.IsSuccessStatusCode) {
            string rsp = await response.Content.ReadAsStringAsync();
            Log.d($"HTTPPOST code: {response.StatusCode}, rsp:{rsp}");
            return rsp;
        } else {
            Log.w($"HTTPPOST Error: {response.StatusCode}");
            return null;
        }
    }
}
