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
@Table(name = "FAMILIA_ANFITRIONA")
@XmlRootElement
@NamedQueries({
    @NamedQuery(name = "FamiliaAnfitriona.findAll", query = "SELECT f FROM FamiliaAnfitriona f")
    , @NamedQuery(name = "FamiliaAnfitriona.findByCodFamilia", query = "SELECT f FROM FamiliaAnfitriona f WHERE f.codFamilia = :codFamilia")
    , @NamedQuery(name = "FamiliaAnfitriona.findByNumIntegrantes", query = "SELECT f FROM FamiliaAnfitriona f WHERE f.numIntegrantes = :numIntegrantes")
    , @NamedQuery(name = "FamiliaAnfitriona.findByNumHabitaciones", query = "SELECT f FROM FamiliaAnfitriona f WHERE f.numHabitaciones = :numHabitaciones")
    , @NamedQuery(name = "FamiliaAnfitriona.findByNumBanos", query = "SELECT f FROM FamiliaAnfitriona f WHERE f.numBanos = :numBanos")
    , @NamedQuery(name = "FamiliaAnfitriona.findByTipoVivienda", query = "SELECT f FROM FamiliaAnfitriona f WHERE f.tipoVivienda = :tipoVivienda")
    , @NamedQuery(name = "FamiliaAnfitriona.findByEstacionamiento", query = "SELECT f FROM FamiliaAnfitriona f WHERE f.estacionamiento = :estacionamiento")
    , @NamedQuery(name = "FamiliaAnfitriona.findByMascotaDescripcion", query = "SELECT f FROM FamiliaAnfitriona f WHERE f.mascotaDescripcion = :mascotaDescripcion")
    , @NamedQuery(name = "FamiliaAnfitriona.findByAnioInscripcion", query = "SELECT f FROM FamiliaAnfitriona f WHERE f.anioInscripcion = :anioInscripcion")})
public class FamiliaAnfitriona implements Serializable {

    private static final long serialVersionUID = 1L;
    // @Max(value=?)  @Min(value=?)//if you know range of your decimal fields consider using these annotations to enforce field validation
    @Id
    @Basic(optional = false)
    @Column(name = "COD_FAMILIA")
    private BigDecimal codFamilia;
    @Basic(optional = false)
    @Column(name = "NUM_INTEGRANTES")
    private BigInteger numIntegrantes;
    @Basic(optional = false)
    @Column(name = "NUM_HABITACIONES")
    private BigInteger numHabitaciones;
    @Basic(optional = false)
    @Column(name = "NUM_BANOS")
    private BigInteger numBanos;
    @Basic(optional = false)
    @Column(name = "TIPO_VIVIENDA")
    private String tipoVivienda;
    @Basic(optional = false)
    @Column(name = "ESTACIONAMIENTO")
    private Character estacionamiento;
    @Basic(optional = false)
    @Column(name = "MASCOTA_DESCRIPCION")
    private String mascotaDescripcion;
    @Basic(optional = false)
    @Column(name = "ANIO_INSCRIPCION")
    private BigInteger anioInscripcion;
    @JoinColumn(name = "FK_COD_ANTECEDENTES", referencedColumnName = "COD_ANTECEDENTES")
    @ManyToOne(optional = false)
    private Antecedentes fkCodAntecedentes;
    @JoinColumn(name = "FK_COD_PERSONA", referencedColumnName = "COD_PERSONA")
    @ManyToOne(optional = false)
    private Persona fkCodPersona;

    public FamiliaAnfitriona() {
    }

    public FamiliaAnfitriona(BigDecimal codFamilia) {
        this.codFamilia = codFamilia;
    }

    public FamiliaAnfitriona(BigDecimal codFamilia, BigInteger numIntegrantes, BigInteger numHabitaciones, BigInteger numBanos, String tipoVivienda, Character estacionamiento, String mascotaDescripcion, BigInteger anioInscripcion) {
        this.codFamilia = codFamilia;
        this.numIntegrantes = numIntegrantes;
        this.numHabitaciones = numHabitaciones;
        this.numBanos = numBanos;
        this.tipoVivienda = tipoVivienda;
        this.estacionamiento = estacionamiento;
        this.mascotaDescripcion = mascotaDescripcion;
        this.anioInscripcion = anioInscripcion;
    }

    public BigDecimal getCodFamilia() {
        return codFamilia;
    }

    public void setCodFamilia(BigDecimal codFamilia) {
        this.codFamilia = codFamilia;
    }

    public BigInteger getNumIntegrantes() {
        return numIntegrantes;
    }

    public void setNumIntegrantes(BigInteger numIntegrantes) {
        this.numIntegrantes = numIntegrantes;
    }

    public BigInteger getNumHabitaciones() {
        return numHabitaciones;
    }

    public void setNumHabitaciones(BigInteger numHabitaciones) {
        this.numHabitaciones = numHabitaciones;
    }

    public BigInteger getNumBanos() {
        return numBanos;
    }

    public void setNumBanos(BigInteger numBanos) {
        this.numBanos = numBanos;
    }

    public String getTipoVivienda() {
        return tipoVivienda;
    }

    public void setTipoVivienda(String tipoVivienda) {
        this.tipoVivienda = tipoVivienda;
    }

    public Character getEstacionamiento() {
        return estacionamiento;
    }

    public void setEstacionamiento(Character estacionamiento) {
        this.estacionamiento = estacionamiento;
    }

    public String getMascotaDescripcion() {
        return mascotaDescripcion;
    }

    public void setMascotaDescripcion(String mascotaDescripcion) {
        this.mascotaDescripcion = mascotaDescripcion;
    }

    public BigInteger getAnioInscripcion() {
        return anioInscripcion;
    }

    public void setAnioInscripcion(BigInteger anioInscripcion) {
        this.anioInscripcion = anioInscripcion;
    }

    public Antecedentes getFkCodAntecedentes() {
        return fkCodAntecedentes;
    }

    public void setFkCodAntecedentes(Antecedentes fkCodAntecedentes) {
        this.fkCodAntecedentes = fkCodAntecedentes;
    }

    public Persona getFkCodPersona() {
        return fkCodPersona;
    }

    public void setFkCodPersona(Persona fkCodPersona) {
        this.fkCodPersona = fkCodPersona;
    }

    @Override
    public int hashCode() {
        int hash = 0;
        hash += (codFamilia != null ? codFamilia.hashCode() : 0);
        return hash;
    }

    @Override
    public boolean equals(Object object) {
        // TODO: Warning - this method won't work in the case the id fields are not set
        if (!(object instanceof FamiliaAnfitriona)) {
            return false;
        }
        FamiliaAnfitriona other = (FamiliaAnfitriona) object;
        if ((this.codFamilia == null && other.codFamilia != null) || (this.codFamilia != null && !this.codFamilia.equals(other.codFamilia))) {
            return false;
        }
        return true;
    }

    @Override
    public String toString() {
        return "modelo.entity.FamiliaAnfitriona[ codFamilia=" + codFamilia + " ]";
    }
    
}
