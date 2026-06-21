import type { PaymentResult } from '../../shared/models.js';

// Classic if-else jungle: every new payment method means editing this file.
// In production this pattern grows past 200 lines; the demo file stays short.

const REQUIRED = ['UPI', 'Card', 'Wallet', 'NetBanking', 'EMI', 'PhonePe'];

// Simulates the prod vault. Set SIMULATE_BLAST_RADIUS=1 to replay the Friday incident.
function loadAllKeys(): Record<string, string> {
  const simulateMissingPhonePe = process.env.SIMULATE_BLAST_RADIUS === '1';
  const keys: Record<string, string> = {};
  for (const method of REQUIRED) {
    if (simulateMissingPhonePe && method === 'PhonePe')
      throw new Error('Missing vault credential: PhonePe (prod deploy forgot to add key)');
    keys[method] = `sk-${method.toLowerCase()}-demo-key`;
  }
  return keys;
}

// Shared config - all providers load keys from one block at MODULE LOAD time.
//    This is the Node/TS analog of C#'s static constructor: if loadAllKeys() throws,
//    importing this module fails, so EVERY payment method goes down - not just PhonePe.
//    (See README "Blast radius across runtimes".)
const GATEWAY_KEYS: Record<string, string> = loadAllKeys();
console.log('  [Before] Gateway keys loaded from vault (shared init OK)');

export class PaymentService {
  process(method: string, amount: number): PaymentResult {
    const apiKey = GATEWAY_KEYS[method]; // every branch depends on the shared dictionary
    if (apiKey === undefined) throw new Error(`Unknown payment method: ${method}`);

    // In production, imagine ten more else-ifs. Each new provider adds a branch here.
    if (method === 'UPI') {
      console.log(`  [Before/UPI] Collect ₹${amount} | VPA verify | key ${apiKey.slice(0, 8)}...`);
      console.log('  [Before/UPI] NPCI rail OK -> merchant credited');
      return { success: true, message: 'UPI collect initiated' };
    } else if (method === 'Card') {
      console.log(`  [Before/Card] Auth hold ₹${amount} | token ${apiKey.slice(0, 8)}... | 3DS=pending`);
      return { success: true, message: 'Card authorized (3DS redirect queued)' };
    } else if (method === 'Wallet') {
      console.log(`  [Before/Wallet] Debit wallet ₹${amount} | balance check | sk=${apiKey.slice(0, 6)}...`);
      return { success: true, message: 'Wallet balance debited' };
    } else if (method === 'NetBanking') {
      console.log(`  [Before] NetBanking redirect | amt=₹${amount} | session key ${apiKey.slice(0, 8)}...`);
      return { success: true, message: 'NetBanking redirect URL generated' };
    } else if (method === 'EMI') {
      console.log(`  [Before/EMI] Tenure split on ₹${amount} | rate lookup | cred ${apiKey.slice(0, 8)}...`);
      return { success: true, message: 'EMI plan attached to order' };
    } else if (method === 'PhonePe') {
      console.log(`  [Before/PhonePe] Intent ₹${amount} | merchantId=${apiKey.slice(0, 8)}... | deep-link pending`);
      return { success: true, message: 'PhonePe payment intent created' };
    }
    throw new Error(`Unknown payment method: ${method}`);
  }
}