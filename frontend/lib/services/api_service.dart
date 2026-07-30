import 'dart:convert';
import 'package:http/http.dart' as http;
import '../models/pet.dart';
import '../models/booking.dart';

class ApiService {
  final String baseUrl = 'http://10.0.2.2:5000/api'; // Emulator localhost

  Future<List<Pet>> getPetsForCustomer(String customerId) async {
    final response = await http.get(Uri.parse('\$baseUrl/customers/\$customerId/pets'));
    if (response.statusCode == 200) {
      final List<dynamic> data = json.decode(response.body);
      return data.map((json) => Pet.fromJson(json)).toList();
    } else {
      throw Exception('Failed to load pets');
    }
  }

  Future<Booking> createBooking(Booking booking) async {
    final response = await http.post(
      Uri.parse('\$baseUrl/bookings'),
      headers: {'Content-Type': 'application/json'},
      body: json.encode(booking.toJson()),
    );
    if (response.statusCode == 201) {
      return Booking.fromJson(json.decode(response.body));
    } else {
      throw Exception('Failed to create booking');
    }
  }
}
