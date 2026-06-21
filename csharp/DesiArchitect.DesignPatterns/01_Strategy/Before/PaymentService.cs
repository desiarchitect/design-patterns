using DesiArchitect.DesignPatterns.Shared;

namespace DesiArchitect.DesignPatterns.Strategy.Before;

// Classic if-else jungle: every new payment method means editing this file.
// In production this pattern grows past 200 lines; the demo file stays short.
public class PaymentService
{
    // All providers load keys from one shared block at startup.
    private static readonly IReadOnlyDictionary<string, string> GatewayKeys;

    static PaymentService()
    {
        GatewayKeys = GatewayVault.LoadAllKeys();
        Console.WriteLine("  [Before] Gateway keys loaded from vault (shared init OK)");
    }

    public PaymentResult Process(string method, decimal amount)
    {
        var apiKey = GatewayKeys[method];   // every branch depends on the shared dictionary

        if (method == "UPI")
        {
            Console.WriteLine($"  [Before/UPI] Collect ₹{amount} | VPA verify | key {apiKey[..8]}...");
            Console.WriteLine("  [Before/UPI] NPCI rail OK -> merchant credited");
            return new PaymentResult(true, "UPI collect initiated");
        }
        else if (method == "Card")
        {
            Console.WriteLine($"  [Before/Card] Auth hold ₹{amount} | token {apiKey[..8]}... | 3DS=pending");
            return new PaymentResult(true, "Card authorized (3DS redirect queued)");
        }
        else if (method == "Wallet")
        {
            Console.WriteLine($"  [Before/Wallet] Debit wallet ₹{amount} | balance check | sk={apiKey[..6]}...");
            return new PaymentResult(true, "Wallet balance debited");
        }
        else if (method == "NetBanking")
        {
            Console.WriteLine($"  [Before] NetBanking redirect | amt=₹{amount} | session key {apiKey[..8]}...");
            return new PaymentResult(true, "NetBanking redirect URL generated");
        }
        else if (method == "EMI")
        {
            Console.WriteLine($"  [Before/EMI] Tenure split on ₹{amount} | rate lookup | cred {apiKey[..8]}...");
            return new PaymentResult(true, "EMI plan attached to order");
        }
        else if (method == "PhonePe")
        {
            Console.WriteLine($"  [Before/PhonePe] Intent ₹{amount} | merchantId={apiKey[..8]}... | deep-link pending");
            return new PaymentResult(true, "PhonePe payment intent created");
        }
        // In production, imagine ten more else-ifs. Each new provider adds a branch and vault key here.
        else
        {
            throw new ArgumentException($"Unknown payment method: {method}");
        }
    }

}

// Simulates prod vault - set SIMULATE_BLAST_RADIUS=1 to replay Friday incident locally
internal static class GatewayVault
{
    private static readonly string[] Required =
        ["UPI", "Card", "Wallet", "NetBanking", "EMI", "PhonePe"];

    public static Dictionary<string, string> LoadAllKeys()
    {
        var simulateMissingPhonePe = Environment.GetEnvironmentVariable("SIMULATE_BLAST_RADIUS") == "1";
        var keys = new Dictionary<string, string>();

        foreach (var method in Required)
        {
            if (simulateMissingPhonePe && method == "PhonePe")
                throw new InvalidOperationException("Missing vault credential: PhonePe (prod deploy forgot to add key)");

            keys[method] = $"sk-{method.ToLowerInvariant()}-demo-key";
        }

        return keys;
    }
}