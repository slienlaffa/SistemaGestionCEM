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
@Table(name = "ANTECEDENTES")
@XmlRootElement
@NamedQueries({
    @NamedQuery(name = "Antecedentes.findAll", query = "SELECT a FROM Antecedentes a")
    , @NamedQuery(name = "Antecedentes.findByCodAntecedentes", query = "SELECT a FROM Antecedentes a WHERE a.codAntecedentes = :codAntecedentes")
    , @NamedQuery(name = "Antecedentes.findByFoto1", query = "SELECT a FROM Antecedentes a WHERE a.foto1 = :foto1")
    , @NamedQuery(name = "Antecedentes.findByFoto2", query = "SELECT a FROM Antecedentes a WHERE a.foto2 = :foto2")
    , @NamedQuery(name = "Antecedentes.findByFoto3", query = "SELECT a FROM Antecedentes a WHERE a.foto3 = :foto3")
    , @NamedQuery(name = "Antecedentes.findByCertResidencia", query = "SELECT a FROM Antecedentes a WHERE a.certResidencia = :certResidencia")
    , @NamedQuery(name = "Antecedentes.findByCertAntecedentes", query = "SELECT a FROM Antecedentes a WHERE a.certAntecedentes = :certAntecedentes")
    , @NamedQuery(name = "Antecedentes.findByCertLaboral", query = "SELECT a FROM Antecedentes a WHERE a.certLaboral = :certLaboral")})
public class Antecedentes implements Serializable {

    private static final long serialVersionUID = 1L;
    // @Max(value=?)  @Min(value=?)//if you know range of your decimal fields consider using these annotations to enforce field validation
    @Id
    @Basic(optional = false)
    @Column(name = "COD_ANTECEDENTES")
    private BigDecimal codAntecedentes;
    @Basic(optional = false)
    @Column(name = "FOTO1")
    private String foto1;
    @Column(name = "FOTO2")
    private String foto2;
    @Column(name = "FOTO3")
    private String foto3;
    @Basic(optional = false)
    @Column(name = "CERT_RESIDENCIA")
    private String certResidencia;
    @Basic(optional = false)
    @Column(name = "CERT_ANTECEDENTES")
    private String certAntecedentes;
    @Basic(optional = false)
    @Column(name = "CERT_LABORAL")
    private String certLaboral;
    @OneToMany(cascade = CascadeType.ALL, mappedBy = "fkCodAntecedentes")
    private List<FamiliaAnfitriona> familiaAnfitrionaList;

    public Antecedentes() {
    }

    public Antecedentes(BigDecimal codAntecedentes) {
        this.codAntecedentes = codAntecedentes;
    }

    public Antecedentes(BigDecimal codAntecedentes, String foto1, String certResidencia, String certAntecedentes, String certLaboral) {
        this.codAntecedentes = codAntecedentes;
        this.foto1 = foto1;
        this.certResidencia = certResidencia;
        this.certAntecedentes = certAntecedentes;
        this.certLaboral = certLaboral;
    }

    public BigDecimal getCodAntecedentes() {
        return codAntecedentes;
    }

    public void setCodAntecedentes(BigDecimal codAntecedentes) {
        this.codAntecedentes = codAntecedentes;
    }

    public String getFoto1() {
        return foto1;
    }

    public void setFoto1(String foto1) {
        this.foto1 = foto1;
    }

    public String getFoto2() {
        return foto2;
    }

    public void setFoto2(String foto2) {
        this.foto2 = foto2;
    }

    public String getFoto3() {
        return foto3;
    }

    public void setFoto3(String foto3) {
        this.foto3 = foto3;
    }

    public String getCertResidencia() {
        return certResidencia;
    }

    public void setCertResidencia(String certResidencia) {
        this.certResidencia = certResidencia;
    }

    public String getCertAntecedentes() {
        return certAntecedentes;
    }

    public void setCertAntecedentes(String certAntecedentes) {
        this.certAntecedentes = certAntecedentes;
    }

    public String getCertLaboral() {
        return certLaboral;
    }

    public void setCertLaboral(String certLaboral) {
        this.certLaboral = certLaboral;
    }

    @XmlTransient
    public List<FamiliaAnfitriona> getFamiliaAnfitrionaList() {
        return familiaAnfitrionaList;
    }

    public void setFamiliaAnfitrionaList(List<FamiliaAnfitriona> familiaAnfitrionaList) {
        this.familiaAnfitrionaList = familiaAnfitrionaList;
    }

    @Override
    public int hashCode() {
        int hash = 0;
        hash += (codAntecedentes != null ? codAntecedentes.hashCode() : 0);
        return hash;
    }

    @Override
    public boolean equals(Object object) {
        // TODO: Warning - this method won't work in the case the id fields are not set
        if (!(object instanceof Antecedentes)) {
            return false;
        }
        Antecedentes other = (Antecedentes) object;
        if ((this.codAntecedentes == null && other.codAntecedentes != null) || (this.codAntecedentes != null && !this.codAntecedentes.equals(other.codAntecedentes))) {
            return false;
        }
        return true;
    }

    @Override
    public String toString() {
        return "modelo.entity.Antecedentes[ codAntecedentes=" + codAntecedentes + " ]";
    }
    
}
