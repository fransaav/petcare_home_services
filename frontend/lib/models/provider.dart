class Provider {
  final String id;
  final String name;
  final String serviceTypes;
  final String serviceArea;
  final int maxDailyCapacity;
  final bool isActive;

  Provider({
    required this.id,
    required this.name,
    required this.serviceTypes,
    required this.serviceArea,
    required this.maxDailyCapacity,
    required this.isActive,
  });

  factory Provider.fromJson(Map<String, dynamic> json) {
    return Provider(
      id: json['id'] ?? '',
      name: json['name'] ?? '',
      serviceTypes: json['serviceTypes'] ?? '',
      serviceArea: json['serviceArea'] ?? '',
      maxDailyCapacity: json['maxDailyCapacity'] ?? 0,
      isActive: json['isActive'] ?? true,
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'name': name,
      'serviceTypes': serviceTypes,
      'serviceArea': serviceArea,
      'maxDailyCapacity': maxDailyCapacity,
      'isActive': isActive,
    };
  }
}
