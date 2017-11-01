/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package WebService;

import java.util.ArrayList;
import java.util.List;
import javax.jws.WebService;
import javax.jws.WebMethod;
import javax.jws.WebParam;

/**
 *
 * @author sergi
 */
@WebService(serviceName = "WebServiceCEM")
public class WebServiceCEM {

    /**
     * This is a sample web service operation
     * @param nombreAlumno
     * @param apellidoAlumno
     * @return 
     */
    @WebMethod(operationName = "obtenerEstadoAlumnos")
    public Alumno obtenerEstadoAlumnos(@WebParam(name = "nombreAlumno") String nombreAlumno, 
            @WebParam(name = "apellidoAlumno") String apellidoAlumno) {
        List<Alumno> alumnos = obtenerAlumnos();
        nombreAlumno = nombreAlumno.trim();
        apellidoAlumno = apellidoAlumno.trim();
        for(Alumno alumno : alumnos){
            if(alumno.nombreAlumno.equalsIgnoreCase(nombreAlumno)
            && alumno.apellidoAlumno.equalsIgnoreCase(apellidoAlumno)){
                return alumno;
            }
        }
        return null;
    }
    
    private List<Alumno> obtenerAlumnos(){
        List<Alumno> alumnos = new ArrayList();
        alumnos.add(new Alumno("Fernando", "Diaz Montenegro", "Regular"));
        alumnos.add(new Alumno("Jennifer", "Letelier", "Regular"));
        alumnos.add(new Alumno("Ben", "Pinkman", "Con Deuda"));
        alumnos.add(new Alumno("Paris", "Cuevas", "Regular"));
        alumnos.add(new Alumno("Tom", "Rodriguez", "Con Deuda"));
        alumnos.add(new Alumno("Jerry", "Colon", "Regular"));
        return alumnos;
    }
    
}