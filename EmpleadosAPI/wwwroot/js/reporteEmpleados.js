document.addEventListener('DOMContentLoaded', () => {
    document.getElementById('generar-reporte').addEventListener('click', generarReporte);
});

async function generarReporte() {
    const fechaInicio = document.getElementById('fechaInicio').value;
    const fechaFin = document.getElementById('fechaFin').value;

    if (!fechaInicio || !fechaFin) {
        alert('Por favor, seleccione ambas fechas');
        return;
    }

    try {
        const response = await fetch(`api/Empleado/reporte?fechaInicio=${fechaInicio}&fechaFin=${fechaFin}`);

        // Verifica si la respuesta no es JSON y maneja el error
        if (!response.ok) {
            throw new Error('Error en la solicitud: ' + response.statusText);
        }

        const empleados = await response.json();
        mostrarReporte(empleados);
    } catch (error) {
        console.error('Error al generar reporte:', error);
        alert('Error al generar el reporte');
    }
}

function mostrarReporte(empleados) {
    const reporteContainer = document.getElementById('reporte-resultado');
    reporteContainer.innerHTML = '';
    const table = document.createElement('table');
    table.innerHTML = `
        <tr>
            <th>Nombres</th>
            <th>DPI</th>
            <th>Edad</th>
            <th>Departamento</th>
            <th>Fecha de Ingreso</th>
            <th>Estatus</th>
        </tr>
    `;

    empleados.forEach(emp => {
        const nombreDepartamento = obtenerNombreDepartamento(emp.departamentoId);
        const row = table.insertRow();
        row.innerHTML = `
            <td>${emp.nombres}</td>
            <td>${emp.dpi}</td>
            <td>${calcularEdadEmpleado(emp.fechaNacimiento)}</td>
            <td>${nombreDepartamento}</td>
            <td>${formatearFecha(emp.fechaIngreso)}</td>
            <td>${emp.estatus ? 'Activo' : 'Inactivo'}</td>
        `;
    });

    reporteContainer.appendChild(table);
    printReport();
}
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


function obtenerNombreDepartamento(codigoDepartamento) {
    const departamento = departamentos.find(dept => dept.codigoDepartamento === codigoDepartamento);
    return departamento ? departamento.nombreDepartamento : 'Desconocido';
}

function calcularEdadEmpleado(fechaNacimiento) {
    const today = new Date();
    const birthDate = new Date(fechaNacimiento);
    let age = today.getFullYear() - birthDate.getFullYear();
    const monthDiff = today.getMonth() - birthDate.getMonth();

    if (monthDiff < 0 || (monthDiff === 0 && today.getDate() < birthDate.getDate())) {
        age--;
    }

    return age;
}

function formatearFecha(fecha) {
    const date = new Date(fecha);
    return `${date.getDate()}/${date.getMonth() + 1}/${date.getFullYear()}`;
}

function printReport() {
    const printWindow = window.open('', '', 'height=600,width=800');
    const content = document.getElementById('reporte-resultado').innerHTML;

    // Maneja el caso en que no hay contenido
    if (!content.trim()) {
        alert('No hay datos para mostrar.');
        return;
    }

    printWindow.document.write('<html><head><title>Reporte de Empleados</title>');
    printWindow.document.write('<link rel="stylesheet" type="text/css" href="css/styles.css">');
    printWindow.document.write('</head><body>');
    printWindow.document.write(content);
    printWindow.document.write('</body></html>');
    printWindow.document.close();
    printWindow.focus();
    printWindow.print();
}
