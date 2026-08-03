import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import '../providers/app_provider.dart';
import 'create_booking_screen.dart';

class PetListScreen extends StatefulWidget {
  @override
  _PetListScreenState createState() => _PetListScreenState();
}

class _PetListScreenState extends State<PetListScreen> {
  @override
  void initState() {
    super.initState();
    WidgetsBinding.instance.addPostFrameCallback((_) {
      Provider.of<AppProvider>(context, listen: false).fetchPets();
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: Text('Mis Mascotas')),
      body: Consumer<AppProvider>(
        builder: (context, provider, child) {
          if (provider.isLoading) {
            return Center(child: CircularProgressIndicator());
          }
          if (provider.pets.isEmpty) {
            return Center(child: Text('No se encontraron mascotas.'));
          }
          return ListView.builder(
            itemCount: provider.pets.length,
            itemBuilder: (context, index) {
              final pet = provider.pets[index];
              return ListTile(
                title: Text(pet.name),
                subtitle: Text('\${pet.species} - \${pet.breed}'),
                trailing: Icon(Icons.pets),
                onTap: () {
                  Navigator.push(
                    context,
                    MaterialPageRoute(
                      builder: (_) => CreateBookingScreen(pet: pet),
                    ),
                  );
                },
              );
            },
          );
        },
      ),
    );
  }
}
