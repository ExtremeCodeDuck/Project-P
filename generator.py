import numpy as np

#Простейший ГСПЧ (ЛИНЕЙНЫЙ КОНГРУЭНТНЫЙ МЕТОД) Базовый генеаратор используемый во всех остальных злоключениях
# X = [Число на усмотрение пользователя] t=232 or 2147483647;  a=134775813 or 16807; c=1 or 0; (Желательные значения дающие больший прекол) 
# Числа для стандартного гспч Delphi or multiplicative linear congruential generator (Stephen Park and Keith Miller)
def LINEAR_CONGRUENT_METHOD(X,t,a,c,s,b,count):
    
    for i in range(count-1):
        if(b):
            if(s==1):
              s=-1
            else:
                s=1

        X.append((X[-1]*a+c)%t*s)
    
    return X
    

# ГПСЧ - равномерное распредиление
#my_random_uniform(X[i],0,1.5258) коэффиценты подобранные опотным путем чтоб навыходе было (0;1] максимально близко 
def my_random_uniform(X,a,b):

    return a + X * (b - a)/10#0x7fff

    



#ГСПЧ - нормальное распредиление
def my_random_normal(r,f):

    z0 = np.cos(2*np.pi*f)*np.power(-2*np.log(r),1/2)
    z1 = np.sin(2*np.pi*f)*np.power(-2*np.log(r),1/2)



    return z0,z1




# ГСПЧ (Базовое - Равномерное - Нормальное)


def getNormsl(count):

    start_array = np.array(LINEAR_CONGRUENT_METHOD([11010000],2147483647,16807,0,1,False,count ))/100000

    uniform_array = []
    for i in range(count):
        uniform_array.append(my_random_uniform(start_array[i],0,1.5258))

    
    uniform_array1 = np.array(uniform_array[0:round(count/2)])/10000
    uniform_array2 = np.array(uniform_array[round(count/2):count])/10000

    
    
    
    if(len(uniform_array1)>len(uniform_array2)):
        uniform_array2 = np.append(uniform_array2,0)
    

    finish_array =[]
    for i in range(round(count/2)):
        
        a = my_random_normal(uniform_array1[i],uniform_array2[i])
        finish_array.append(a[0])
        finish_array.append(a[1])

    if(len(finish_array)>count):
        finish_array.pop(len(finish_array)-1)
    
    return np.array(finish_array)




#ГСПЧ (Базовое - Нормальное - Равномерное)

def getUniform(count,a,b):

    start_array = np.array(LINEAR_CONGRUENT_METHOD([110100],2147483647,16807,0,1.528,False,count ))/10000000000
    

    start_array1 = np.array(start_array[0:round(count/2)])
    start_array2 = np.array(start_array[round(count/2):count])

    normal_array = []
    for i in range(round(count/2)):
        
        z = my_random_normal(start_array1[i],start_array2[i])
        normal_array.append(z[0])
        normal_array.append(z[1])

    if(len(normal_array)>count):
        normal_array.pop(len(normal_array)-1)

    uniform_array = []
    for i in range(count):
        uniform_array.append(my_random_uniform(normal_array[i],a,b))

    return np.array(uniform_array)


