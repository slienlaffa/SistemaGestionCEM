/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package modelo.entity;

import java.io.Serializable;
import java.math.BigDecimal;
import java.math.BigInteger;
import javax.persistence.Basic;
import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.Id;
import javax.persistence.JoinColumn;
import javax.persistence.ManyToOne;
import javax.persistence.NamedQueries;
import javax.persistence.NamedQuery;
import javax.persistence.Table;
import javax.xml.bind.annotation.XmlRootElement;

/**
 *
 * @author felipe
 */
@Entity
@Table(name = "CENTRO_ESTUDIO_LOCAL")
@XmlRootElement
@NamedQueries({
    @NamedQuery(name = "CentroEstudioLocal.findAll", query = "SELECT c FROM CentroEstudioLocal c")
    , @NamedQuery(name = "CentroEstudioLocal.findByCodCel", query = "SELECT c FROM CentroEstudioLocal c WHERE c.codCel = :codCel")
    , @NamedQuery(name = "CentroEstudioLocal.findByNombreCentro", query = "SELECT c FROM CentroEstudioLocal c WHERE c.nombreCentro = :nombreCentro")
    , @NamedQuery(name = "CentroEstudioLocal.findByNomDirector", query = "SELECT c FROM CentroEstudioLocal c WHERE c.nomDirector = :nomDirector")
    , @NamedQuery(name = "CentroEstudioLocal.findByAreaEspecializacion", query = "SELECT c FROM CentroEstudioLocal c WHERE c.areaEspecializacion = :areaEspecializacion")
    , @NamedQuery(name = "CentroEstudioLocal.findByDireccion", query = "SELECT c FROM CentroEstudioLocal c WHERE c.direccion = :direccion")
    , @NamedQuery(name = "CentroEstudioLocal.findByCorreo", query = "SELECT c FROM CentroEstudioLocal c WHERE c.correo = :correo")
    , @NamedQuery(name = "CentroEstudioLocal.findByTelefono", query = "SELECT c FROM CentroEstudioLocal c WHERE c.telefono = :telefono")
    , @NamedQuery(name = "CentroEstudioLocal.findByDescripcion", query = "SELECT c FROM CentroEstudioLocal c WHERE c.descripcion = :descripcion")})
public class CentroEstudioLocal implements Serializable {

    private static final long serialVersionUID = 1L;
    // @Max(value=?)  @Min(value=?)//if you know range of your decimal fields consider using these annotations to enforce field validation
    @Id
    @Basic(optional = false)
    @Column(name = "COD_CEL")
    private BigDecimal codCel;
    @Basic(optional = false)
    @Column(name = "NOMBRE_CENTRO")
    private String nombreCentro;
    @Basic(optional = false)
    @Column(name = "NOM_DIRECTOR")
    private String nomDirector;
    @Basic(optional = false)
    @Column(name = "AREA_ESPECIALIZACION")
    private String areaEspecializacion;
    @Basic(optional = false)
    @Column(name = "DIRECCION")
    private String direccion;
    @Basic(optional = false)
    @Column(name = "CORREO")
    private String correo;
    @Basic(optional = false)
    @Column(name = "TELEFONO")
    private BigInteger telefono;
    @Basic(optional = false)
    @Column(name = "DESCRIPCION")
    private String descripcion;
    @JoinColumn(name = "FK_COD_CIUDAD", referencedColumnName = "COD_CIUDAD")
    @ManyToOne(optional = false)
    private Ciudad fkCodCiudad;

    public CentroEstudioLocal() {
    }

    public CentroEstudioLocal(BigDecimal codCel) {
        this.codCel = codCel;
    }

    public CentroEstudioLocal(BigDecimal codCel, String nombreCentro, String nomDirector, String areaEspecializacion, String direccion, String correo, BigInteger telefono, String descripcion) {
        this.codCel = codCel;
        this.nombreCentro = nombreCentro;
        this.nomDirector = nomDirector;
        this.areaEspecializacion = areaEspecializacion;
        this.direccion = direccion;
        this.correo = correo;
        this.telefono = telefono;
        this.descripcion = descripcion;
    }

    public BigDecimal getCodCel() {
        return codCel;
    }

    public void setCodCel(BigDecimal codCel) {
        this.codCel = codCel;
    }

    public String getNombreCentro() {
        return nombreCentro;
    }

    public void setNombreCentro(String nombreCentro) {
        this.nombreCentro = nombreCentro;
    }

    public String getNomDirector() {
        return nomDirector;
    }

    public void setNomDirector(String nomDirector) {
        this.nomDirector = nomDirector;
    }

    public String getAreaEspecializacion() {
        return areaEspecializacion;
    }

    public void setAreaEspecializacion(String areaEspecializacion) {
        this.areaEspecializacion = areaEspecializacion;
    }

    public String getDireccion() {
        return direccion;
    }

    public void setDireccion(String direccion) {
        this.direccion = direccion;
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

    public String getDescripcion() {
        return descripcion;
    }

    public void setDescripcion(String descripcion) {
        this.descripcion = descripcion;
    }

    public Ciudad getFkCodCiudad() {
        return fkCodCiudad;
    }

    public void setFkCodCiudad(Ciudad fkCodCiudad) {
        this.fkCodCiudad = fkCodCiudad;
    }

    @Override
    public int hashCode() {
        int hash = 0;
        hash += (codCel != null ? codCel.hashCode() : 0);
        return hash;
    }

    @Override
    public boolean equals(Object object) {
        // TODO: Warning - this method won't work in the case the id fields are not set
        if (!(object instanceof CentroEstudioLocal)) {
            return false;
        }
        CentroEstudioLocal other = (CentroEstudioLocal) object;
        if ((this.codCel == null && other.codCel != null) || (this.codCel != null && !this.codCel.equals(other.codCel))) {
            return false;
        }
        return true;
    }

    @Override
    public String toString() {
        return "modelo.entity.CentroEstudioLocal[ codCel=" + codCel + " ]";
    }
    
}
