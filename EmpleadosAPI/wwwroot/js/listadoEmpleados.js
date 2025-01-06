document.addEventListener('DOMContentLoaded', async () => {
    const response = await fetch('api/Empleado');
    const empleados = await response.json();
    const employeeTableBody = document.querySelector('#employee-table tbody');

    empleados.forEach(emp => {
        const nombreDepartamento = obtenerNombreDepartamento(emp.departamentoId);
        const row = document.createElement('tr');
        row.innerHTML = `
            <td>${emp.nombres}</td>
            <td>${emp.dpi}</td>
            <td>${calcularEdad(emp.fechaNacimiento)}</td>
            <td>${nombreDepartamento}</td>
            <td>${formatearFecha(emp.fechaIngreso)}</td>
            <td>${determinarEstatus(emp.fechaIngreso)}</td>
            <td><button onclick="window.location.href='actualizar.html?id=${emp.id}'">Actualizar</button></td>
        `;
        employeeTableBody.appendChild(row);
    });
});

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

function calcularEdad(fechaNacimiento) {
    const hoy = new Date();
    const nacimiento = new Date(fechaNacimiento);
    let edad = hoy.getFullYear() - nacimiento.getFullYear();
    const diferenciaMeses = hoy.getMonth() - nacimiento.getMonth();
    if (diferenciaMeses < 0 || (diferenciaMeses === 0 && hoy.getDate() < nacimiento.getDate())) {
        edad--;
    }
    return edad;
}

function formatearFecha(fecha) {
    const date = new Date(fecha);
    return date.toLocaleDateString();
}

function determinarEstatus(fechaIngreso) {
    const hoy = new Date();
    const ingreso = new Date(fechaIngreso);
    const diferenciaMeses = (hoy.getFullYear() - ingreso.getFullYear()) * 12 + hoy.getMonth() - ingreso.getMonth();

    if (diferenciaMeses < 3) {
        return 'Periodo de prueba';
    } else if (diferenciaMeses >= 3 && diferenciaMeses < 12) {
        return 'Empleado regular';
    } else {
        return 'Empleado permanente';
    }
}
