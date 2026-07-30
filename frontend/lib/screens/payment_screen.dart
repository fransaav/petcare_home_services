import 'package:flutter/material.dart';
import '../models/booking.dart';

class PaymentScreen extends StatelessWidget {
  final Booking booking;

  PaymentScreen({required this.booking});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: Text('Confirmación de Pago')),
      body: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.stretch,
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            Icon(Icons.check_circle_outline, size: 100, color: Colors.green),
            SizedBox(height: 24),
            Text(
              'Reserva Confirmada',
              textAlign: TextAlign.center,
              style: Theme.of(context).textTheme.headlineMedium,
            ),
            SizedBox(height: 16),
            Text(
              'Servicio: \${booking.serviceType}',
              textAlign: TextAlign.center,
            ),
            Text(
              'Modalidad: \${booking.modality}',
              textAlign: TextAlign.center,
            ),
            Text(
              'Total: \$\${booking.totalCost}',
              textAlign: TextAlign.center,
              style: TextStyle(fontWeight: FontWeight.bold, fontSize: 18),
            ),
            Spacer(),
            ElevatedButton(
              onPressed: () {
                Navigator.popUntil(context, (route) => route.isFirst);
              },
              child: Text('Volver al Inicio'),
            ),
          ],
        ),
      ),
    );
  }
}
