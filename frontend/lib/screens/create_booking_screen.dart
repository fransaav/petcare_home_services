import 'package:flutter/material.dart';
import '../models/pet.dart';
import '../models/booking.dart';
import '../services/api_service.dart';
import 'payment_screen.dart';

class CreateBookingScreen extends StatefulWidget {
  final Pet pet;

  CreateBookingScreen({required this.pet});

  @override
  _CreateBookingScreenState createState() => _CreateBookingScreenState();
}

class _CreateBookingScreenState extends State<CreateBookingScreen> {
  final _apiService = ApiService();
  String _selectedService = 'Grooming';
  String _selectedModality = 'HomeVisit';
  bool _isLoading = false;

  final List<String> _services = ['Grooming', 'Walking', 'Vet', 'Boarding'];
  final List<String> _modalities = ['HomeVisit', 'PickupDropoff'];

  void _submitBooking() async {
    setState(() {
      _isLoading = true;
    });

    try {
      final booking = Booking(
        id: '',
        customerId: widget.pet.customerId,
        petId: widget.pet.id,
        providerId: '00000000-0000-0000-0000-000000000000', // Dummy
        serviceType: _selectedService,
        modality: _selectedModality,
        scheduledTime: DateTime.now().add(Duration(days: 1)),
        status: 'Pending',
        totalCost: 50.0, // Fixed demo cost
      );

      final createdBooking = await _apiService.createBooking(booking);
      
      Navigator.pushReplacement(
        context,
        MaterialPageRoute(
          builder: (_) => PaymentScreen(booking: createdBooking),
        ),
      );
    } catch (e) {
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(content: Text('Error al crear reserva: \$e')),
      );
    } finally {
      setState(() {
        _isLoading = false;
      });
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: Text('Nueva Reserva - \${widget.pet.name}')),
      body: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.stretch,
          children: [
            DropdownButtonFormField<String>(
              value: _selectedService,
              items: _services.map((s) => DropdownMenuItem(value: s, child: Text(s))).toList(),
              onChanged: (val) => setState(() => _selectedService = val!),
              decoration: InputDecoration(labelText: 'Tipo de Servicio'),
            ),
            SizedBox(height: 16),
            DropdownButtonFormField<String>(
              value: _selectedModality,
              items: _modalities.map((m) => DropdownMenuItem(value: m, child: Text(m))).toList(),
              onChanged: (val) => setState(() => _selectedModality = val!),
              decoration: InputDecoration(labelText: 'Modalidad'),
            ),
            Spacer(),
            _isLoading
                ? Center(child: CircularProgressIndicator())
                : ElevatedButton(
                    onPressed: _submitBooking,
                    child: Text('Confirmar Reserva'),
                  ),
          ],
        ),
      ),
    );
  }
}
