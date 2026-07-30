class Pet {
  final String id;
  final String name;
  final String species;
  final String breed;
  final double weightKg;
  final String specialRequirements;
  final bool hasVaccinationRecord;
  final String customerId;

  Pet({
    required this.id,
    required this.name,
    required this.species,
    required this.breed,
    required this.weightKg,
    required this.specialRequirements,
    required this.hasVaccinationRecord,
    required this.customerId,
  });

  factory Pet.fromJson(Map<String, dynamic> json) {
    return Pet(
      id: json['id'] ?? '',
      name: json['name'] ?? '',
      species: json['species'] ?? '',
      breed: json['breed'] ?? '',
      weightKg: (json['weightKg'] ?? 0).toDouble(),
      specialRequirements: json['specialRequirements'] ?? '',
      hasVaccinationRecord: json['hasVaccinationRecord'] ?? false,
      customerId: json['customerId'] ?? '',
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'name': name,
      'species': species,
      'breed': breed,
      'weightKg': weightKg,
      'specialRequirements': specialRequirements,
      'hasVaccinationRecord': hasVaccinationRecord,
      'customerId': customerId,
    };
  }
}
