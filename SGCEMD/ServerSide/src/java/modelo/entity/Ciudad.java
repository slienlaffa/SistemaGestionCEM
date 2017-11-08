/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package modelo.entity;

import java.io.Serializable;
import java.math.BigDecimal;
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
@Table(name = "CIUDAD")
@XmlRootElement
@NamedQueries({
    @NamedQuery(name = "Ciudad.findAll", query = "SELECT c FROM Ciudad c")
    , @NamedQuery(name = "Ciudad.findByCodCiudad", query = "SELECT c FROM Ciudad c WHERE c.codCiudad = :codCiudad")
    , @NamedQuery(name = "Ciudad.findByDescripcion", query = "SELECT c FROM Ciudad c WHERE c.descripcion = :descripcion")})
public class Ciudad implements Serializable {

    private static final long serialVersionUID = 1L;
    // @Max(value=?)  @Min(value=?)//if you know range of your decimal fields consider using these annotations to enforce field validation
    @Id
    @Basic(optional = false)
    @Column(name = "COD_CIUDAD")
    private BigDecimal codCiudad;
    @Basic(optional = false)
    @Column(name = "DESCRIPCION")
    private String descripcion;
    @OneToMany(cascade = CascadeType.ALL, mappedBy = "fkCodCiudad")
    private List<CentroEstudioLocal> centroEstudioLocalList;
    @OneToMany(cascade = CascadeType.ALL, mappedBy = "fkCodCiudad")
    private List<Persona> personaList;
    @JoinColumn(name = "FK_COD_PAIS", referencedColumnName = "COD_PAIS")
    @ManyToOne(optional = false)
    private Pais fkCodPais;

    public Ciudad() {
    }

    public Ciudad(BigDecimal codCiudad) {
        this.codCiudad = codCiudad;
    }

    public Ciudad(BigDecimal codCiudad, String descripcion) {
        this.codCiudad = codCiudad;
        this.descripcion = descripcion;
    }

    public BigDecimal getCodCiudad() {
        return codCiudad;
    }

    public void setCodCiudad(BigDecimal codCiudad) {
        this.codCiudad = codCiudad;
    }

    public String getDescripcion() {
        return descripcion;
    }

    public void setDescripcion(String descripcion) {
        this.descripcion = descripcion;
    }

    @XmlTransient
    public List<CentroEstudioLocal> getCentroEstudioLocalList() {
        return centroEstudioLocalList;
    }

    public void setCentroEstudioLocalList(List<CentroEstudioLocal> centroEstudioLocalList) {
        this.centroEstudioLocalList = centroEstudioLocalList;
    }

    @XmlTransient
    public List<Persona> getPersonaList() {
        return personaList;
    }

    public void setPersonaList(List<Persona> personaList) {
        this.personaList = personaList;
    }

    public Pais getFkCodPais() {
        return fkCodPais;
    }

    public void setFkCodPais(Pais fkCodPais) {
        this.fkCodPais = fkCodPais;
    }

    @Override
    public int hashCode() {
        int hash = 0;
        hash += (codCiudad != null ? codCiudad.hashCode() : 0);
        return hash;
    }

    @Override
    public boolean equals(Object object) {
        // TODO: Warning - this method won't work in the case the id fields are not set
        if (!(object instanceof Ciudad)) {
            return false;
        }
        Ciudad other = (Ciudad) object;
        if ((this.codCiudad == null && other.codCiudad != null) || (this.codCiudad != null && !this.codCiudad.equals(other.codCiudad))) {
            return false;
        }
        return true;
    }

    @Override
    public String toString() {
        return "modelo.entity.Ciudad[ codCiudad=" + codCiudad + " ]";
    }
    
}
