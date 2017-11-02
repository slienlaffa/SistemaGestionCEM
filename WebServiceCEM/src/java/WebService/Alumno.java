/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package WebService;

/**
 *
 * @author sergi
 */
public class Alumno {
    public int codigoAlumno;
    public String nombreAlumno;
    public String apellidoAlumno;
    public String estadoAlumno;
    
    public Alumno(){
        
    }
    public Alumno(String nombreAlumno, String apellidoAlumno, String estadoAlumno){
        this.nombreAlumno = nombreAlumno;
        this.apellidoAlumno = apellidoAlumno;
        this.estadoAlumno = estadoAlumno;
    }
}
