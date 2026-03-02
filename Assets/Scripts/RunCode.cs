using System;
using System.Security.Cryptography;
using System.Text;

public static class RunCodeGenerator
{
    private static readonly byte[] Key = Encoding.UTF8.GetBytes("PRWy7XycpT2mrcYA");
    private const ulong Const64 = 0x1234567890ABCDEF;

    public static string GenerateRunCode()
    {
        // 1. 获取当前 Unix 毫秒时间戳
        long unixMs = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        // 2. 构造 128-bit 明文块：前 8 字节 = 时间戳，后 8 字节 = 常量
        byte[] tBytes = BitConverter.GetBytes(unixMs);
        byte[] cBytes = BitConverter.GetBytes(Const64);

        // BitConverter 在小端机器上需要反转
        if (BitConverter.IsLittleEndian)
        {
            Array.Reverse(tBytes);
            Array.Reverse(cBytes);
        }

        byte[] plaintext = new byte[16];
        Buffer.BlockCopy(tBytes, 0, plaintext, 0, 8);
        Buffer.BlockCopy(cBytes, 0, plaintext, 8, 8);

        // 3. AES-128-ECB 加密（无 Padding）
        using Aes aes = Aes.Create();
        aes.Key = Key;
        aes.Mode = CipherMode.ECB;
        aes.Padding = PaddingMode.None;

        using ICryptoTransform encryptor = aes.CreateEncryptor();
        byte[] cipher = encryptor.TransformFinalBlock(plaintext, 0, 16);

        // 4. Base64 URL-safe 编码（去掉 '='）
        string s = Convert.ToBase64String(cipher)
            .Replace('+', '-')
            .Replace('/', '_')
            .TrimEnd('=');

        return s;
    }
}
