// Shared domain types - mirror of the C# Shared/Models.cs

export interface PaymentResult {
  success: boolean;
  message: string;
}

export interface Order {
  id: string;
  phone: string;
  status: string;
}
