import 'package:flutter/material.dart';
import '../models/pet.dart';
import '../services/api_service.dart';

class AppProvider with ChangeNotifier {
  final ApiService _apiService = ApiService();
  final String currentCustomerId = '00000000-0000-0000-0000-000000000000'; // Dummy ID for MVP

  List<Pet> _pets = [];
  bool _isLoading = false;

  List<Pet> get pets => _pets;
  bool get isLoading => _isLoading;

  Future<void> fetchPets() async {
    _isLoading = true;
    notifyListeners();

    try {
      _pets = await _apiService.getPetsForCustomer(currentCustomerId);
    } catch (e) {
      _pets = [];
    } finally {
      _isLoading = false;
      notifyListeners();
    }
  }
}
