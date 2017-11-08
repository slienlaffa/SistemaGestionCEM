/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package modelo.entity;

import java.io.Serializable;
import java.math.BigDecimal;
import java.math.BigInteger;
import java.util.List;
import javax.persistence.Basic;
import javax.persistence.CascadeType;
import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.Id;
import javax.persistence.JoinColumn;
import javax.persistence.ManyToOne;
import javax.persistence.NamedQueries;
import javax.persistence.NamedQuery;
import javax.persistence.OneToMany;
import javax.persistence.Table;
import javax.xml.bind.annotation.XmlRootElement;
import javax.xml.bind.annotation.XmlTransient;

/**
 *
 * @author felipe
 */
@Entity
@Table(name = "PERSONA")
@XmlRootElement
@NamedQueries({
    @NamedQuery(name = "Persona.findAll", query = "SELECT p FROM Persona p")
    , @NamedQuery(name = "Persona.findByCodPersona", query = "SELECT p FROM Persona p WHERE p.codPersona = :codPersona")
    , @NamedQuery(name = "Persona.findByNombre", query = "SELECT p FROM Persona p WHERE p.nombre = :nombre")
    , @NamedQuery(name = "Persona.findByApellido", query = "SELECT p FROM Persona p WHERE p.apellido = :apellido")
    , @NamedQuery(name = "Persona.findByCorreo", query = "SELECT p FROM Persona p WHERE p.correo = :correo")
    , @NamedQuery(name = "Persona.findByTelefono", query = "SELECT p FROM Persona p WHERE p.telefono = :telefono")
    , @NamedQuery(name = "Persona.findByNacionalidad", query = "SELECT p FROM Persona p WHERE p.nacionalidad = :nacionalidad")})
public class Persona implements Serializable {

    private static final long serialVersionUID = 1L;
    // @Max(value=?)  @Min(value=?)//if you know range of your decimal fields consider using these annotations to enforce field validation
    @Id
    @Basic(optional = false)
    @Column(name = "COD_PERSONA")
    private BigDecimal codPersona;
    @Basic(optional = false)
    @Column(name = "NOMBRE")
    private String nombre;
    @Basic(optional = false)
    @Column(name = "APELLIDO")
    private String apellido;
    @Basic(optional = false)
    @Column(name = "CORREO")
    private String correo;
    @Basic(optional = false)
    @Column(name = "TELEFONO")
    private BigInteger telefono;
    @Basic(optional = false)
    @Column(name = "NACIONALIDAD")
    private String nacionalidad;
    @OneToMany(cascade = CascadeType.ALL, mappedBy = "fkCodPersona")
    private List<FamiliaAnfitriona> familiaAnfitrionaList;
    @OneToMany(cascade = CascadeType.ALL, mappedBy = "fkCodPersona")
    private List<Alumno> alumnoList;
    @JoinColumn(name = "FK_COD_CIUDAD", referencedColumnName = "COD_CIUDAD")
    @ManyToOne(optional = false)
    private Ciudad fkCodCiudad;
    @JoinColumn(name = "FK_COD_GENERO", referencedColumnName = "COD_GENERO")
    @ManyToOne(optional = false)
    private Genero fkCodGenero;
    @JoinColumn(name = "FK_COD_USUARIO", referencedColumnName = "COD_USUARIO")
    @ManyToOne(optional = false)
    private Usuario fkCodUsuario;

    public Persona() {
    }

    public Persona(BigDecimal codPersona) {
        this.codPersona = codPersona;
    }

    public Persona(BigDecimal codPersona, String nombre, String apellido, String correo, BigInteger telefono, String nacionalidad) {
        this.codPersona = codPersona;
        this.nombre = nombre;
        this.apellido = apellido;
        this.correo = correo;
        this.telefono = telefono;
        this.nacionalidad = nacionalidad;
    }

    public BigDecimal getCodPersona() {
        return codPersona;
    }

    public void setCodPersona(BigDecimal codPersona) {
        this.codPersona = codPersona;
    }

    public String getNombre() {
        return nombre;
    }

    public void setNombre(String nombre) {
        this.nombre = nombre;
    }

    public String getApellido() {
        return apellido;
    }

    public void setApellido(String apellido) {
        this.apellido = apellido;
    }

    public String getCorreo() {
        return correo;
    }

    public void setCorreo(String correo) {
        this.correo = correo;
    }

    public BigInteger getTelefono() {
        return telefono;
    }

    public void setTelefono(BigInteger telefono) {
        this.telefono = telefono;
    }

    public String getNacionalidad() {
        return nacionalidad;
    }

    public void setNacionalidad(String nacionalidad) {
        this.nacionalidad = nacionalidad;
    }

    @XmlTransient
    public List<FamiliaAnfitriona> getFamiliaAnfitrionaList() {
        return familiaAnfitrionaList;
    }

    public void setFamiliaAnfitrionaList(List<FamiliaAnfitriona> familiaAnfitrionaList) {
        this.familiaAnfitrionaList = familiaAnfitrionaList;
    }

    @XmlTransient
    public List<Alumno> getAlumnoList() {
        return alumnoList;
    }

    public void setAlumnoList(List<Alumno> alumnoList) {
        this.alumnoList = alumnoList;
    }

    public Ciudad getFkCodCiudad() {
        return fkCodCiudad;
    }

    public void setFkCodCiudad(Ciudad fkCodCiudad) {
        this.fkCodCiudad = fkCodCiudad;
    }

    public Genero getFkCodGenero() {
        return fkCodGenero;
    }

    public void setFkCodGenero(Genero fkCodGenero) {
        this.fkCodGenero = fkCodGenero;
    }

    public Usuario getFkCodUsuario() {
        return fkCodUsuario;
    }

    public void setFkCodUsuario(Usuario fkCodUsuario) {
        this.fkCodUsuario = fkCodUsuario;
    }

    @Override
    public int hashCode() {
        int hash = 0;
        hash += (codPersona != null ? codPersona.hashCode() : 0);
        return hash;
    }

    @Override
    public boolean equals(Object object) {
        // TODO: Warning - this method won't work in the case the id fields are not set
        if (!(object instanceof Persona)) {
            return false;
        }
        Persona other = (Persona) object;
        if ((this.codPersona == null && other.codPersona != null) || (this.codPersona != null && !this.codPersona.equals(other.codPersona))) {
            return false;
        }
        return true;
    }

    @Override
    public String toString() {
        return "modelo.entity.Persona[ codPersona=" + codPersona + " ]";
    }
    
}
