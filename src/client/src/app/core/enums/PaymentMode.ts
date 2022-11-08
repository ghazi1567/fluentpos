export enum PaymentMode {
    Cash = 0,
    Bank = 1
}
export const PaymentModeMapping: Record<PaymentMode, string> = {
    [PaymentMode.Cash]: "Cash",
    [PaymentMode.Bank]: "Bank"
};
