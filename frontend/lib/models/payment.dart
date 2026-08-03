class Payment {
  final String id;
  final String bookingId;
  final double amount;
  final DateTime paymentDate;
  final String status;
  final String paymentMethod;

  Payment({
    required this.id,
    required this.bookingId,
    required this.amount,
    required this.paymentDate,
    required this.status,
    required this.paymentMethod,
  });

  factory Payment.fromJson(Map<String, dynamic> json) {
    return Payment(
      id: json['id'] ?? '',
      bookingId: json['bookingId'] ?? '',
      amount: (json['amount'] ?? 0).toDouble(),
      paymentDate: DateTime.parse(json['paymentDate']),
      status: json['status'] ?? 'Pending',
      paymentMethod: json['paymentMethod'] ?? '',
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'bookingId': bookingId,
      'amount': amount,
      'paymentDate': paymentDate.toIso860String(),
      'status': status,
      'paymentMethod': paymentMethod,
    };
  }
}
