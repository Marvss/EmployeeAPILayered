
document.addEventListener('DOMContentLoaded', function () {
    cargarDepartamentos();

    const empleadoForm = document.getElementById('empleado-form');
    empleadoForm.addEventListener('submit', async function (event) {
        event.preventDefault();

        const formData = new FormData(empleadoForm);
        const data = {
            nombres: formData.get('nombres'),
            dpi: formData.get('dpi'),
            fechaNacimiento: formData.get('fechaNacimiento'),
            sexo: formData.get('sexo'),
            fechaIngreso: formData.get('fechaIngreso'),
            direccion: formData.get('direccion'),
            nit: formData.get('nit'),
            departamentoId: parseInt(formData.get('departamentoId')) // Convertir a número
        };

        try {
            const response = await fetch('/api/Empleado', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(data)
            });

            if (response.ok) {
                alert('Empleado guardado exitosamente');
            } else {
                alert('Error al guardar el empleado');
            }
        } catch (error) {
            console.error('Error:', error);
            alert('Error al guardar el empleado');
        }
    });
});

function cargarDepartamentos() {
    const departamentos = [
        { codigoDepartamento: 1, nombreDepartamento: "Créditos y Cobros" },
        { codigoDepartamento: 2, nombreDepartamento: "Publicidad e Imagen Corporativa" },
        { codigoDepartamento: 3, nombreDepartamento: "Gerencia General" },
        { codigoDepartamento: 4, nombreDepartamento: "Finanzas" },
        { codigoDepartamento: 5, nombreDepartamento: "Mercadeo" },
        { codigoDepartamento: 6, nombreDepartamento: "Contabilidad" },
        { codigoDepartamento: 7, nombreDepartamento: "Negocios" },
        { codigoDepartamento: 8, nombreDepartamento: "Administración" },
        { codigoDepartamento: 9, nombreDepartamento: "Informática" },
        { codigoDepartamento: 10, nombreDepartamento: "Corporativo" },
        { codigoDepartamento: 11, nombreDepartamento: "Riesgos" },
        { codigoDepartamento: 12, nombreDepartamento: "Ventas" }
    ];

    const departamentoSelect = document.getElementById('departamentoId');
    departamentos.forEach(dept => {
        const option = document.createElement('option');
        option.value = dept.codigoDepartamento;
        option.textContent = dept.nombreDepartamento;
        departamentoSelect.appendChild(option);
    });
}