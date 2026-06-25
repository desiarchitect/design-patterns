// Boolean flags for each charge; every new fee adds another if-block
export class BillCalculator {
  calculateBill(
    itemTotal: number,
    hasPackaging: boolean,
    hasDelivery: boolean,
    hasPlatformFee: boolean,
    hasSurge: boolean,
    hasConvenienceFee = false,
  ): number {
    let total = itemTotal;
    console.log(`  [Before] Item total: ₹${total}`);

    if (hasPackaging) {
      total += 15;
      console.log(`  [Before] + Packaging: ₹15 -> ₹${total}`);
    }
    if (hasDelivery) {
      total += 40;
      console.log(`  [Before] + Delivery: ₹40 -> ₹${total}`);
    }
    if (hasPlatformFee) {
      total += 5;
      console.log(`  [Before] + Platform fee: ₹5 -> ₹${total}`);
    }
    if (hasSurge) {
      total += 20;
      console.log(`  [Before] + Surge: ₹20 -> ₹${total}`);
    }
    if (hasConvenienceFee) {
      total += 5;
      console.log(`  [Before] + Convenience fee: ₹5 -> ₹${total}`);
    }
    // Testing? 2^5 = 32 combinations. Overlap: platform + convenience both add fee.

    return total;
  }
}