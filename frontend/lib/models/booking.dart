class Booking {
  final String id;
  final String customerId;
  final String petId;
  final String providerId;
  final String serviceType;
  final String modality;
  final DateTime scheduledTime;
  final String status;
  final double totalCost;

  Booking({
    required this.id,
    required this.customerId,
    required this.petId,
    required this.providerId,
    required this.serviceType,
    required this.modality,
    required this.scheduledTime,
    required this.status,
    required this.totalCost,
  });

  factory Booking.fromJson(Map<String, dynamic> json) {
    return Booking(
      id: json['id'] ?? '',
      customerId: json['customerId'] ?? '',
      petId: json['petId'] ?? '',
      providerId: json['providerId'] ?? '',
      serviceType: json['serviceType'] ?? '',
      modality: json['modality'] ?? '',
      scheduledTime: DateTime.parse(json['scheduledTime']),
      status: json['status'] ?? 'Pending',
      totalCost: (json['totalCost'] ?? 0).toDouble(),
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'customerId': customerId,
      'petId': petId,
      'providerId': providerId,
      'serviceType': serviceType,
      'modality': modality,
      'scheduledTime': scheduledTime.toIso860String(),
      'status': status,
      'totalCost': totalCost,
    };
  }
}
