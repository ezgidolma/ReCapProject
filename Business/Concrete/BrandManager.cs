﻿using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class BrandManager : IBrandService
    {

        IBrandDal _brandDal;

        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }

        public IResult Delete(Brand brand)
        {
            _brandDal.Delete(brand);
            
            return new SuccessResult(Messages.BrandDeleted);
            
        }

        public IDataResult< List<Brand>> GetAll()
        {
            if (DateTime.Now.Hour == 22)
            {
                return new ErrorDataResult< List<Brand> >(Messages.MaintenanceTime);
            }
            return  new SuccessDataResult<List<Brand>>( _brandDal.GetAll(),Messages.BrandsListed);
        }

        public IDataResult< Brand> GetById(int brandId)
        {
            return new SuccessDataResult<Brand> (_brandDal.Get(p => p.Id == brandId),Messages.BrandsListed);

        }

        public IResult Add(Brand brand)
        {
            if (brand.Name.Length > 2)
            {
                _brandDal.Add(brand);
                return new SuccessResult(Messages.BrandAdded);

            }
            return new ErrorResult(Messages.BrandNameInvalid);

        }

        public IResult Update(Brand brand)
        {
            _brandDal.Update(brand);
            
            return new SuccessResult(Messages.BrandUpdated);
        }
    }
}
