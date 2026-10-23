// Payout helpers for the Merchant Help Center.
// Money is handled as integer satang: 1 baht = 100 satang.

export function toSatang(amountBaht) {
  const [baht, satang = '0'] = String(amountBaht).split('.');
  if (!/^\d+$/.test(baht) || !/^\d*$/.test(satang) || satang.length > 2) {
    throw new Error(`Invalid amount: ${amountBaht}`);
  }
  return Number(baht) * 100 + Number(satang.padEnd(2, '0'));
}

export function payoutTotal(amountsBaht) {
  return amountsBaht.reduce((sum, a) => sum + toSatang(a), 0);
}
