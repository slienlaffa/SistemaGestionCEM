/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package modelo.rest;

import java.util.List;
import javax.ejb.Stateless;
import javax.persistence.EntityManager;
import javax.persistence.PersistenceContext;
import modelo.entity.Alumno;

/**
 *
 * @author felipe
 */
@Stateless
public class AlumnoFacade {

    @PersistenceContext(unitName = "ServerSidePU")
    private EntityManager em;

    public List<Alumno> findAll() {
      return em.createQuery("select c from Alumno c").getResultList();
    }

    public List<Alumno> findByName(String name) {
      return em.createQuery("select c from Alumno c where UPPER(c.nombre) LIKE :nombreAlumno").
                             setParameter("nombreAlumno", "%" + name.toUpperCase() + "%").getResultList();
    }
}
