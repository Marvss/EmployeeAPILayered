document.addEventListener('DOMContentLoaded', () => {
    const urlParams = new URLSearchParams(window.location.search);
    const employeeId = urlParams.get('id');

    if (employeeId) {
        fetchEmployeeData(employeeId);
    }

    const form = document.getElementById('update-form');
    form.addEventListener('submit', handleUpdate);
});

async function fetchEmployeeData(id) {
    try {
        const response = await fetch(`api/Empleado/${id}`);
        if (!response.ok) throw new Error('Network response was not ok');
        const empleado = await response.json();

        document.getElementById('employee-id').value = empleado.id;
        document.getElementById('nombres').value = empleado.nombres;
        document.getElementById('dpi').value = empleado.dpi;
        document.getElementById('fechaNacimiento').value = empleado.fechaNacimiento.split('T')[0];
        document.getElementById('sexo').value = empleado.sexo;
        document.getElementById('fechaIngreso').value = empleado.fechaIngreso.split('T')[0];
        document.getElementById('direccion').value = empleado.direccion;
        document.getElementById('nit').value = empleado.nit;
        document.getElementById('departamentoId').value = empleado.departamentoId;
        document.getElementById('estatus').value = empleado.estatus ? '1' : '0';

    } catch (error) {
        console.error('Error fetching employee data:', error);
        alert('Error al cargar los datos del empleado');
    }
}

async function handleUpdate(event) {
    event.preventDefault();

    const id = document.getElementById('employee-id').value;
    const nombres = document.getElementById('nombres').value;
    const dpi = document.getElementById('dpi').value;
    const fechaNacimiento = document.getElementById('fechaNacimiento').value;
    const sexo = document.getElementById('sexo').value;
    const fechaIngreso = document.getElementById('fechaIngreso').value;
    const direccion = document.getElementById('direccion').value;
    const nit = document.getElementById('nit').value;
    const departamentoId = parseInt(document.getElementById('departamentoId').value);
    const estatus = document.getElementById('estatus').value === '1';

    // Calculate Edad
    const edad = calculateAge(fechaNacimiento);

    const empleado = {
        id: parseInt(id),
        nombres,
        dpi,
        fechaNacimiento,
        sexo,
        fechaIngreso,
        edad,
        direccion,
        nit,
        departamentoId,
        estatus
    };

    try {
        const response = await fetch(`api/Empleado/${id}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(empleado)
        });

        if (!response.ok) throw new Error('Network response was not ok');
        alert('Empleado actualizado con éxito');
        window.location.href = 'index.html'; // Redirigir a la lista de empleados
    } catch (error) {
        console.error('Error actualizando el empleado:', error);
        alert('Error al actualizar el empleado');
    }
}

function calculateAge(fechaNacimiento) {
    const today = new Date();
    const birthDate = new Date(fechaNacimiento);
    let age = today.getFullYear() - birthDate.getFullYear();
    const monthDifference = today.getMonth() - birthDate.getMonth();
    if (monthDifference < 0 || (monthDifference === 0 && today.getDate() < birthDate.getDate())) {
        age--;
    }
    return age;
}
